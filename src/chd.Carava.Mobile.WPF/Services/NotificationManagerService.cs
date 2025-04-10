using chd.Caravan.Mobile.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.WPF.Services
{
    public class NotificationManagerService : INotificationManagerService
    {
        public event EventHandler<NotificationEventArgs> NotificationReceived;

        public const string IdKey = "intentid";

        private int messageId = 0;

        public NotificationManagerService()
        {
        }

      
        public void ReceiveNotification(NotificationEventArgs args)
        {
            this.NotificationReceived?.Invoke(this, args);
        }

        public void SendNotification<TData>(string title, string message, TData data, bool cancel = true)
        => this.SendNotification(title, message, cancel);


        public void SendNotification(string title, string message, bool cancel = true)
        {
           
        }
    }
}
