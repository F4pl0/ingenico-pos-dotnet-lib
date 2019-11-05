using System;
using System.Collections.Generic;
using System.Text;

namespace IngenicoPOS {
    class Utils {

        // Store Field Separator for easy use
        private static string FS = ((char)0x1c).ToString();

        public static string BuildMessage( ECRMessage msg ) {
            // Assemble the message according to protocol
            string message =
                "00" // Identifier
                + msg.TerminalID.ToString("D2") // Terminal ID
                + "00" // Source ID
                + msg.NextTransactionNo.ToString("D4") // Sequential number
                + msg.TransactionType // Transaction type
                + (msg.POSPrints ? "1" : "0") // Printer flag
                + msg.CashierID.ToString("D2") // Cashier ID
                + FS
                + (msg.TransactionAmount != 0 ? "" + msg.TransactionAmount : "") // Transaction amount 1
                + FS + FS
                + "+0" // Amount Exponent
                + FS
                + (msg.CurrencyISO > 0 ? msg.CurrencyISO.ToString("D3") : "") // Amount currency
                + FS + FS + FS + FS
                + msg.AuthorizationCode // Authorization code
                + FS + FS + FS
                + msg.InputLabel // Input label
                + FS
                + msg.InsurancePolicyNumber // Insurance policy number
                + FS
                + msg.InstallmentsNumber // Installments number
                + FS + FS
                + msg.LanguageID // Language from Consts.Language
                + FS
                + msg.PrintData // Data that should be printed on receipt.
                + FS + FS
                + (msg.TransactionAmountCash != 0 ? "" + msg.TransactionAmountCash : "") // In case of Sale+Cash transaction this field contains Cash amount.
                + FS
                + msg.PayservicesData // TLV based data
                + FS
                + msg.TransactionActivationCode // Transaction Activation Code for Mobile Payment
                + FS
                + msg.InstantPaymentRef // Reference number for Instant Payment
                + FS
                + msg.QRCodeData // QR code data for Instant Payment
                + FS
                + ((char)0x03).ToString();

            message = ((char)0x02).ToString() + message + (char)GetLRC(Encoding.ASCII.GetBytes(message));
            return message;
        }
        private static int GetLRC(byte[] message) {
            int LRC = 0;
            foreach (byte a in message) {
                LRC ^= a;
            }
            return LRC;
        }
    }
}
