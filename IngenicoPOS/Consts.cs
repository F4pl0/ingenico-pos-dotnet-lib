using System;
using System.Collections.Generic;
using System.Text;

namespace IngenicoPOS {
    public static class Consts {
        public static class TransactionType {
            public const string
                INITIALIZATION = "00",
                SALE = "01",
                PREAUTHOTRIZATION = "02",
                CASH_ADVANCE = "03",
                PREAUTHORIZATION_COMPLETION = "04",
                REFUND = "05",
                OFFLINE_SALE = "06",
                SALE_PLUS_CASH = "07",
                PAYSERVICES = "08",
                BALANCE_INQURY = "09",
                VOID_TRANSACTION = "10",
                CANCEL_PREV_TRANSACTION = "11",
                VOID_OF_AUTH_PREAUTH = "12",
                SALE_WITH_INSTALLMENTS = "20",
                SETTLEMENT = "30",
                BATCH_STATUS = "31",
                CURRENT_TOTALS = "32",
                CURRENT_ISSUER_TOTALS = "33",
                CURRENT_ISSUER_TRANS_AND_TOTALS = "34",
                READ_PERSONAL_CARD_DATA = "78",
                READ_VEHICLE_CARD_DATA = "79",
                PIN_VERIFY_BEGIN = "80",
                PIN_VERIFY_END = "81",
                RESART_TERMINAL = "82",
                SOCAR_TRANSACTION = "88",
                INSTANT_PAYMENT_REFUND = "89",
                INSTANT_PAYMENT_INQUIRY = "90",
                READ_PAYMENT_CARD = "91",
                AS24_FLEET_CARD_TRANS_COMPLETION = "92",
                AS24_FLEET_CARD_TRANS = "93",
                BUS_PLUS_LOYALTY_CHECK = "94",
                SLEEP_TRANSACTION = "95",
                GET_TERMINAL_ID = "96",
                PRINT_DATA_TRANSACTION = "97",
                CARD_READ = "98",
                INPUT_DATA = "99";
        }
        public static class TransactionFlag {
            public const string
                ERROR = "00",
                ACCEPTED_WITHOUT_AUTH = "01",
                ACCEPTED_WITH_AUTH = "02",
                REFUSED = "04",
                TERMINAL_ID = "05",
                COMMUNICATION_ERROR = "06",
                SLEEP_MODE = "07";
        }
        public static class Language {
            public const string
                DEFAULT = "00",
                MACEDONIAN = "01",
                SERBIAN = "02",
                ENGLISH = "03",
                ROMANIAN = "04",
                MONTENEGRO = "05",
                BOSNIAN = "06",
                CROATIAN = "07",
                SLOVENE = "08",
                ALBANIAN = "09",
                BULGARIAN = "10",
                FRENCH = "11",
                SLOVAK = "12",
                GERMAN = "13",
                ITALIAN = "14",
                SPANISH = "15",
                MALAGASY = "16";
        }

        public static class CardDataSource {
            public const string
                MAGNETIC_STRIPE = "M",
                SMART_CARD = "S",
                MANUAL_PAN_ENTRY = "K",
                CONTACTLESS = "C",
                INSTANT_PAYMENT = "I";
        }
    }
}
