using System;

namespace WeatherAlerts.EventHandler.Alerts
{
    public class NotificationEventArgs : EventArgs
    {
        public Locations Location { get; set; }
        public WeatherTypes WeatherType { get; set; }
        public NotificationEventArgs(Locations location, WeatherTypes weatherType)
        {
            Location = location;
            WeatherType = weatherType;
        }
    }
}
