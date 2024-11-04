using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Contracts.Contants
{
    public static class EndpointContants
    {
        public const string ROOT = "/api";
        public static class Ruuvi
        {
            public const string ROOT = "ruuvi";
            public const string ALL = "";
            public const string GET_DATA = "{id:int}";
        }

        public static class Votronic
        {
            public const string ROOT = "votronic";
            public const string Battery = "battery";
            public const string Solar = "solar";
        }
        public static class Pi
        {
            public const string ROOT = "pi";
            public const string GET_SETTING = "settings";
            public const string GET_PIN_STATE= "pinstate/pin:int";
            public const string POST_WRITE_PIN = "write";
        }
        public static class Aes
        {
            public const string ROOT ="aes";

            public const string IS_ACTIVE = "isactive";
            public const string AES_OFF_SINCE = "aesoffsince";
            public const string BATTERY_LIMIT = "batterylimit";
            public const string AES_TIMEOUT = "aestimeout";
            public const string AES_LIMIT = "solaraeslimit";

            public const string SET_ACTIVE = "setactive";
            public const string CHECK_ACTIVE = "checkactive";
            public const string AES_OFF = "aesoff";
        }
    }
}
