using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace chd.Caravan.Mobile.Extensions
{
    public static class HttpsClientHandlerService
    {

        public static HttpMessageHandler GetPlatformMessageHandler() => new AndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
        };

    }
}
