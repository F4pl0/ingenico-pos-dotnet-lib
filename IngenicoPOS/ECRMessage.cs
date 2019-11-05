using System;
using System.Collections.Generic;
using System.Text;

namespace IngenicoPOS {
    public class ECRMessage {

        #region Vars

        private int
            _terminalID,
            _nextTransactionNo,
            _cashierID,
            _currencyISO;

        private Int64
            _transactionAmount,
            _transactionAmountCash;

        private string
            _transactionType,
            _authorizationCode,
            _inputLabel,
            _insurancePolicyNumber,
            _installmentsNumber,
            _languageID,
            _printData,
            _payservicesData,
            _transactionActivationCode,
            _instantPaymentRef,
            _QRCodeData;

        private bool
            _POSPrints;

        #endregion

        #region Getters & Setters

        #region int Getters & Setters
        public int TerminalID { get { return _terminalID; } set { _terminalID = value; } }
        public int NextTransactionNo { get { return _nextTransactionNo; } set { _nextTransactionNo = value; } }
        public int CashierID { get { return _cashierID; } set { _cashierID = value; } }
        public int CurrencyISO { get { return _currencyISO; } set { _currencyISO = value; } }
        #endregion

        #region Int64 Getters & Setters
        public Int64 TransactionAmount { get { return _transactionAmount; } set { _transactionAmount = value; } }
        public Int64 TransactionAmountCash { get { return _transactionAmountCash; } set { _transactionAmountCash = value; } }
        #endregion

        #region string Getters & Setters
        public string TransactionType { get { return _transactionType; } set { _transactionType = value; } }
        public string AuthorizationCode { get { return _authorizationCode; } set { _authorizationCode = value; } }
        public string InputLabel { get { return _inputLabel; } set { _inputLabel = value; } }
        public string InsurancePolicyNumber { get { return _insurancePolicyNumber; } set { _insurancePolicyNumber = value; } }
        public string InstallmentsNumber { get { return _installmentsNumber; } set { _installmentsNumber = value; } }
        public string LanguageID { get { return _languageID; } set { _languageID = value; } }
        public string PrintData { get { return _printData; } set { _printData = value; } }
        public string PayservicesData { get { return _payservicesData; } set { _payservicesData = value; } }
        public string TransactionActivationCode { get { return _transactionActivationCode; } set { _transactionActivationCode = value; } }
        public string InstantPaymentRef { get { return _instantPaymentRef; } set { _instantPaymentRef = value; } }
        public string QRCodeData { get { return _QRCodeData; } set { _QRCodeData = value; } }
        #endregion

        #region bool Getters & Setters
        public bool POSPrints { get { return _POSPrints; } set { _POSPrints = value; } }
        #endregion

        #endregion

        public string Message {
            get {
                return Utils.BuildMessage(this);
            }
        }
        public ECRMessage() {

        }
    }
}