using System;
using WeatherAlerts.EventHandler.Alerts;

namespace WeatherAlerts.EventHandler.Notifications
{
    public class SMSNotificationsSender : INotificationSender
    {
        public string Target { get; set; }

        public SMSNotificationsSender(string target)
        {
            Target = target;
        }

        public void SendNotification(object sender, NotificationEventArgs notificationEventArgs)
        {
            Console.WriteLine("");
            if (sender is Alert alert)
            {
                Console.WriteLine($"Notification triggered by Alert: {alert.Id.ToString().Substring(0, 8)}");
            }
            Console.WriteLine($"Sending SMS Notification to {Target}");
            Console.WriteLine($"Expect {notificationEventArgs.WeatherType} at {notificationEventArgs.Location}!");
        }
    }
}
