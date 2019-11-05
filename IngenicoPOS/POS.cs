using System;
using System.IO.Ports;

namespace IngenicoPOS {

    public class POS {
        private SerialPort POSPort;
        private bool _connected = false;

        public int NextTransactionNo = 0;
        public bool POSPrints = true;
        public int CurrencyISO = 941;
        public int CashierID = 0;
        public string Language = "00";

        private bool receivedACK = false;
        private bool receivedNACK = false;
        private POSMessage lastPOSMsg;

        public bool IsConnected { get { return _connected; } }

        public POS(string Port, int baud = 115200) {
            // Initialize The Serial Port
            POSPort = new SerialPort(Port, baud, Parity.None, 8, StopBits.One);
            // Assign DataReceived event
            POSPort.DataReceived += new SerialDataReceivedEventHandler(pos_DataReceived);
        }

        public SaleResult Sale(Int64 Amount) {
            if (!_connected)
                return new SaleResult(false, null);

            // Build the message to send to the device
            ECRMessage msg = new ECRMessage();
            msg.NextTransactionNo = NextTransactionNo;
            msg.CurrencyISO = CurrencyISO;
            msg.POSPrints = POSPrints;
            msg.CashierID = CashierID;
            msg.TransactionAmount = Amount;
            msg.TransactionType = Consts.TransactionType.SALE;
            msg.LanguageID = Language;

            // Clear the buffer
            POSPort.ReadExisting();
            // Send the message to the device
            POSPort.Write(msg.Message);

            /// To verify that the device got our message, we wait for ACK or 0x06 response
            /// If the device got our message, and it was not in correct format, we will get 0x15 as response, which is NACK
            /// We have allowed time for the device to get back to us and that, by specification, is 1 second
            /// After 1 second we send it the message once again, and hopefully it will receive it this time
            /// After 3 tries, the transaction will be marked as unsuccessful, with communication error.

            int sendTry = 0;
            int ms = 0;
            bool success = false;

            #region Waiting for ACK/NACK

            while (true) {
                if (receivedACK) {
                    success = true;
                    receivedACK = false;
                    receivedNACK = false;
                    break;
                }
                if (receivedNACK) {
                    success = false;
                    receivedACK = false;
                    receivedNACK = false;
                    break;
                }
                if (ms > 1000) {
                    ms = 0;
                    sendTry++;
                    if (sendTry == 3)
                        break;
                    POSPort.Write(msg.Message);
                }
                System.Threading.Thread.Sleep(1);
                ms++;
            }

            #endregion Waiting for ACK/NACK

            if (!success)
                return new SaleResult(false, null);

            /// After we receive 0x06, we wait for the message on the end of the transaction that tells us
            /// wether the transaction was successful or not, and other params such as cause, transaction date, amount, time etc...

            #region Waiting for Message

            lastPOSMsg = null;
            while (lastPOSMsg == null) { }

            #endregion Waiting for Message

            /// Message received, check it and send the response if message is correct and
            /// if transaction was successful
            if (lastPOSMsg.TransactionFlag == Consts.TransactionFlag.ACCEPTED_WITH_AUTH ||
                    lastPOSMsg.TransactionFlag == Consts.TransactionFlag.ACCEPTED_WITHOUT_AUTH) {
                // Transaction was successful :D
                // Send ACK and return transaction successful
                POSPort.Write(((char)0x06).ToString());
            } else if (lastPOSMsg.TransactionFlag == Consts.TransactionFlag.REFUSED ||
                        lastPOSMsg.TransactionFlag == Consts.TransactionFlag.ERROR ||
                        lastPOSMsg.TransactionFlag == Consts.TransactionFlag.COMMUNICATION_ERROR) {
                // Transaction wasn't successful :(
                // Send ACK and return transaction unsuccessful
                POSPort.Write(((char)0x06).ToString());
                return new SaleResult(false, lastPOSMsg);
            } else {
                // Message probably not valid, send NACK
                POSPort.Write(((char)0x15).ToString());
                return new SaleResult(false, null);
            }

            // If everything goes as planned, this lines of code should be executed
            NextTransactionNo++;
            return new SaleResult(true, lastPOSMsg);
        }

        public bool Connect() {
            try {
                POSPort.Open();
                POSPort.ReadExisting(); // Clear the buffer
            } catch (Exception) {
                // In unlikely case of the port being used or some error..
                _connected = false;
                return false;
            }
            _connected = true;
            return true;
        }

        public void Disonnect() {
            try {
                POSPort.Close();
            } catch (Exception) { }
            _connected = false;
        }

        private void pos_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            string message = ((SerialPort)sender).ReadExisting();
            // Debug.WriteLine(BitConverter.ToString(System.Text.Encoding.Default.GetBytes(message)));  // Write HEX to the debug

            if (message == ("\u0006"))
                receivedACK = true; // Received ACK, set the flag
            else if (message == ("\u0015"))
                receivedNACK = true; // Received NACK, set the flag
            else if (message.StartsWith("25")) {
                POSPort.Write(((char)0x06).ToString()); // Received HOLD Message, send ACK to confirm the hold
            } else {
                lastPOSMsg = new POSMessage(message); // Received POSMessage, Parse it and assign it to lastPOSMsg
            }
        }
    }
}