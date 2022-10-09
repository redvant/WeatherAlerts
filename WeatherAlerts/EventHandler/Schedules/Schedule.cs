using System;
using System.Collections.Generic;
using WeatherAlerts.EventHandler.Notifications;
using WeatherAlerts.EventHandler.Alerts;
using System.Timers;

namespace WeatherAlerts.EventHandler.Schedules
{
    public class Schedule
    {
        private readonly List<Alert> _alerts;

        private Timer _timer;

        public Schedule()
        {
            _alerts = new List<Alert>();
        }

        public Alert CreateAlert(DateTime triggerTime, WeatherTypes weatherLookout, Locations locationlookout, INotificationSender notificationSender)
        {
            Alert alert = new Alert(triggerTime, weatherLookout, locationlookout, notificationSender);
            _alerts.Add(alert);
            return alert;
        }

        public void RunSchedule()
        {
            _timer = new Timer();
            _timer.Interval = 1000;

            _timer.Elapsed += OnTimedEvent;

            _timer.AutoReset = true;

            _timer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit the program at any time... ");
            Console.ReadLine();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            foreach (Alert alert in _alerts)
            {
                if (alert.TriggerTime.CompareTo(e.SignalTime) < 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Raising Alerts at {e.SignalTime}");
                    alert.Trigger();
                    _alerts.Remove(alert);
                }
            }
        }
    }
}
