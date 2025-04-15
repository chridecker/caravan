using System.Diagnostics;

namespace chd.CaraVan.Contracts.Contants
{
    public static class EndpointContants
    {
        public const string ROOT = "/api";

        public static class System
        {
            public const string ROOT = "system";

            public const string REBOOT = "reboot";
            public const string CHANGE_STATE_IN_TIME = "changestateintime";
            public const string START_SERVICE = "startservice";
            public const string STOP_SERVICE = "stopservice";
            public const string RUNNING_SINCE = "runningsince";
        }

        public static class Control
        {
            public const string ROOT = "control";

            public const string GET_SETTINGS = "GetSettings";
            public const string POST_SETTINGS = "SetSettings";

        }
    }
}
