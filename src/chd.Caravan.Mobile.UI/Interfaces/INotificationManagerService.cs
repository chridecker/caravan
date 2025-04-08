using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Interfaces
{
   public interface INotificationManagerService
    {
        event EventHandler<NotificationEventArgs> NotificationReceived;
        void SendNotification(string title, string message, bool autoCloseOnLick = true);
        void SendNotification<TData>(string title, string message, TData data, bool autoCloseOnLick = true);
        void ReceiveNotification(NotificationEventArgs dto);
    }
    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(int id, string title, string message, object data, bool cancel = true)
        {
            this.Id = id;
            this.Title = title;
            this.Message = message;
            this.Data = data;
            this.Cancel = cancel;
        }
        public int Id { get; set; }
        public string Title { get; }
        public string Message { get; }
        public object Data { get; set; }
        public bool Cancel { get; set; }
    }
}
