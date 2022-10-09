using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherAlerts
{
    static class EventExample
    {
        public static void Test()
        {
            ScheduleE schedule = new ScheduleE();
            AlertE alert = schedule.CreateAlert(DateTime.Now, WeatherTypes.Rain, schedule.EmailNotificationSender);
            schedule.TriggerAlert(alert.Id);

            schedule.ChangeNotificationMethod(alert.Id, schedule.SmsNotificationSender);
            schedule.TriggerAlert(alert.Id);

            AlertE alert2 = schedule.CreateAlert(DateTime.Now, WeatherTypes.Snow, schedule.SmsNotificationSender);
            schedule.TriggerAlert(alert2.Id);
        }
    }

    public class AlertEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
        public WeatherTypes WeatherType { get; set; }
        public AlertEventArgs(DateTime time, WeatherTypes weatherType)
        {
            Time = time;
            WeatherType = weatherType;
        }
    }

    public class ScheduleE
    {
        public INotificationSenderE EmailNotificationSender { get; set; }
        public INotificationSenderE SmsNotificationSender { get; set; }
        private readonly IList<AlertE> _alerts;

        public ScheduleE()
        {
            EmailNotificationSender = new EmailNotificationsSenderE();
            SmsNotificationSender = new SMSNotificationsSenderE();
            _alerts = new List<AlertE>();
        }

        public AlertE CreateAlert(DateTime triggerTime, WeatherTypes weatherLookout, INotificationSenderE notificationSender)
        {
            AlertE alert = new AlertE(triggerTime, weatherLookout, notificationSender);
            _alerts.Add(alert);
            return alert;
        }

        public void TriggerAlert(Guid id)
        {
            AlertE alert = _alerts.FirstOrDefault<AlertE>(alert => alert.Id == id);
            alert.Trigger();
        }

        public void ChangeNotificationMethod(Guid id, INotificationSenderE notificationSender)
        {
            AlertE alert = _alerts.FirstOrDefault<AlertE>(alert => alert.Id == id);
            alert.ChangeNotificationMethod(notificationSender);
        }
    }

    public class AlertE
    {
        public Guid Id { get; set; }
        public DateTime TriggerTime { get; set; }
        public WeatherTypes WeatherLookout { get; set; }
        public INotificationSenderE NotificationSender { get; set; }

        private delegate void ExecuteAlertDelegate(object sender, AlertEventArgs eventArgs);
        private event ExecuteAlertDelegate _alertEvent;
        private event ExecuteAlertDelegate AlertEvent { add { _alertEvent += value; } remove { _alertEvent -= value; } }

        public AlertE(DateTime triggerTime, WeatherTypes weatherLookout, INotificationSenderE notificationSender)
        {
            Id = Guid.NewGuid();
            TriggerTime = triggerTime;
            WeatherLookout = weatherLookout;
            NotificationSender = notificationSender;

            AlertEvent += RunAlertHandler;
            AlertEvent += NotificationSender.SendNotification;
        }
        public void RunAlertHandler(object sender, AlertEventArgs alertEventArgs)
        {
            Console.WriteLine("");
            if (sender is AlertE alert)
            {
                Console.WriteLine($"Trigger of Alert with id: {alert.Id}");
            }
            Console.WriteLine($"Getting Weather Forecast at {alertEventArgs.Time}...");
            Console.WriteLine($"Checking for {alertEventArgs.WeatherType}...");
        }
        public void Trigger()
        {
            _alertEvent.Invoke(this, new AlertEventArgs(TriggerTime, WeatherLookout));
        }

        public void ChangeNotificationMethod(INotificationSenderE newNotificationSender)
        {
            AlertEvent -= NotificationSender.SendNotification;
            NotificationSender = newNotificationSender;
            AlertEvent += NotificationSender.SendNotification;
        }
    }

    public interface INotificationSenderE
    {
        void SendNotification(object sender, AlertEventArgs alertEventArgs);
    }

    public class EmailNotificationsSenderE : INotificationSenderE
    {
        public void SendNotification(object sender, AlertEventArgs alertEventArgs)
        {
            Console.WriteLine($"Sending Email Notification for {alertEventArgs.WeatherType} Forecast");
        }
    }

    public class SMSNotificationsSenderE : INotificationSenderE
    {
        public void SendNotification(object sender, AlertEventArgs alertEventArgs)
        {
            Console.WriteLine($"Sending SMS Notification for {alertEventArgs.WeatherType} Forecast");
        }
    }
}

