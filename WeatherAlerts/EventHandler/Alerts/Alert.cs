using System;
using WeatherAlerts.EventHandler.Notifications;
using WeatherAlerts.EventHandler.Schedules;

namespace WeatherAlerts.EventHandler.Alerts
{
    public class Alert
    {
        public Guid Id { get; set; }
        public DateTime TriggerTime { get; set; }
        public WeatherTypes WeatherLookout { get; set; }
        public Locations LocationLookout { get; set; }

        private event EventHandler<NotificationEventArgs> _notificationEventHandler;
        private event EventHandler<NotificationEventArgs> NotificationEventHandler { add { _notificationEventHandler += value; } remove { _notificationEventHandler -= value; } }

        public Alert(DateTime triggerTime, WeatherTypes weatherLookout,Locations location, INotificationSender notificationSender)
        {
            Id = Guid.NewGuid();
            TriggerTime = triggerTime;
            WeatherLookout = weatherLookout;
            LocationLookout = location;

            AddNotificationMethod(notificationSender);
        }
        public void Trigger()
        {
            Console.WriteLine("");
            Console.WriteLine($"Trigger of Alert with id: {Id}");
            Console.WriteLine($"Getting Weather Forecast for {LocationLookout}...");
            WeatherTypes forecast = GetForecast(LocationLookout, WeatherTypes.Rain);
            Console.WriteLine($"Checking for {WeatherLookout}...");
            if (forecast == WeatherLookout)
            {
                _notificationEventHandler.Invoke(this, new NotificationEventArgs(LocationLookout, WeatherLookout));
            }
        }

        private WeatherTypes GetForecast(Locations location, WeatherTypes weather)
        {
            return weather;
        }

        internal void AddNotificationMethod(INotificationSender notificationSender)
        {
            NotificationEventHandler += notificationSender.SendNotification;
        }
    }
}
