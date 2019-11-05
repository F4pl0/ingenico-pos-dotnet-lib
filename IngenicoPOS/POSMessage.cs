using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IngenicoPOS {
    public class POSMessage {
        private int
            _terminalID,
            _sourceID,
            _sequentialNumber,
            _transactionNumber,
            _batchNumber,
            _amountCurrency;

        private Int64
            _transactionAmount,
            _TIDNumber,
            _MIDNumber,
            _PINBlock,
            _transactionAmountCash,
            _DCCNumber,
            _DCCAmount;

        private string
            _transactionType,
            _transactionFlag,
            _transactionDate,
            _transactionTime,
            _cardDataSource,
            _cardNumber,
            _expirationDate,
            _authorizationCode,
            _companyName,
            _displayMessage,
            _inputData,
            _EMVData,
            _acquirerName,
            _debitTransactionCount,
            _debitTransactionAmount,
            _refundTransactionCount,
            _refundTransactionAmount,
            _installmentsNumber,
            _fullResponseCode,
            _transactionStatus,
            _SPDHTerminalTotals,
            _SPDHHostTotals,
            _cardholderName,
            _RRN,
            _payservicesData,
            _availableBalance,
            _loyaltyData,
            _formattedTTP,
            _DCCProvider,
            _DCCExchangeRate,
            _DCCExchangeRateDate,
            _DCCMarkUpPercent,
            _DCCDisclaimer,
            _DCCStatus,
            _DCCCurrencySymbol,
            _InstantPaymentReference,
            _PersonalVehicleCardData;

        private bool
            _signatureLinePrintFlag,
            _moreMessagesFlag,
            _PINFlag;

        public string TransactionFlag { get { return _transactionFlag; } }
        public string TransactionType { get { return _transactionType; } }
        public Int64 TransactionAmount { get { return _transactionAmount; } }
        public string TransactionDate { get { return _transactionDate; } }
        public string TransactionTime { get { return _transactionTime; } }
        public string CardDataSource { get { return _cardDataSource; } }
        public string AuthorizationCode { get { return _authorizationCode; } }


        public POSMessage() {
        }
        public POSMessage(string parseData) {
            ParseAssign(parseData);
        }

        public void ParseAssign(string parseData) {
            string[] parseArr = parseData.Split((char)0x1c);
            _terminalID = int.Parse(parseArr[0].Substring(3, 2));
            _sequentialNumber = int.Parse(parseArr[0].Substring(7, 4));
            _transactionType = parseArr[0].Substring(11, 2);
            _transactionFlag = parseArr[0].Substring(13, 2);
            _transactionNumber = int.Parse(parseArr[0].Substring(15, 6));
            _batchNumber = int.Parse(parseArr[0].Substring(21, 4));
            _transactionDate = parseArr[0].Substring(25, 6);
            _transactionTime = parseArr[0].Substring(31, 6);
            // FS
            _transactionAmount = Int64.Parse(parseArr[1]);
            // FS FS FS
            _amountCurrency = int.Parse(parseArr[4]);
            // FS
            _cardDataSource = parseArr[5];
            // FS
            _cardNumber = parseArr[6];
            // FS
            _expirationDate = parseArr[7];
            // FS FS FS
            _authorizationCode = parseArr[10];

            /*
             * For some reason, the POS is not sending 
            // FS
            _TIDNumber = Int64.Parse(parseArr[11]);
            // FS
            _MIDNumber = Int64.Parse(parseArr[12]);
            // FS
            _companyName = parseArr[13];
            // FS FS FS FS FS FS FS
            _displayMessage = parseArr[20];
            // FS FS
            _inputData = parseArr[22];
            // FS
            _EMVData = parseArr[23];
            // FS
            _signatureLinePrintFlag = ( parseArr[24] == "1" );
            // FS
            _acquirerName = parseArr[25].Substring(0, 10);
            _debitTransactionCount = parseArr[25].Substring(10,4);
            _debitTransactionAmount = parseArr[25].Substring(14, 12);
            _refundTransactionCount = parseArr[25].Substring(26, 4);
            _refundTransactionAmount = parseArr[25].Substring(30, 12);
            // FS
            _moreMessagesFlag = ( parseArr[26] == "1" );
            // FS
            _installmentsNumber = parseArr[27];
            // FS
            _fullResponseCode = parseArr[28];
            // FS
            _transactionStatus = parseArr[29];
            // FS
            _SPDHTerminalTotals = parseArr[30];
            */
        }
    }
}

