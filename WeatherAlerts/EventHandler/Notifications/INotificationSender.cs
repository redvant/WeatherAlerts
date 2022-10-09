using WeatherAlerts.EventHandler.Alerts;

namespace WeatherAlerts.EventHandler.Notifications
{
    public interface INotificationSender
    {
        string Target { get; set; }
        void SendNotification(object sender, NotificationEventArgs alertEventArgs);
    }
}
