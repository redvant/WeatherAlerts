using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherAlerts
{
    static class DelegateExample
    {
        public static void Test()
        {
            ScheduleD schedule = new ScheduleD();
            AlertD alert = schedule.CreateAlert(new DateTime(), WeatherTypes.Rain, schedule.EmailNotificationSender);
            schedule.AddNotificationMethod(alert.Id, schedule.SmsNotificationSender);
            schedule.TriggerAlert(alert.Id);

            AlertD alert2 = schedule.CreateAlert(new DateTime(), WeatherTypes.Snow, schedule.SmsNotificationSender);
            schedule.TriggerAlert(alert2.Id);
        }
    }

    public class ScheduleD
    {
        public INotificationSenderD EmailNotificationSender { get; set; }
        public INotificationSenderD SmsNotificationSender { get; set; }
        private readonly IList<AlertD> _alerts;

        public ScheduleD()
        {
            EmailNotificationSender = new EmailNotificationsSenderD();
            SmsNotificationSender = new SMSNotificationsSenderD();
            _alerts = new List<AlertD>();
        }

        public AlertD CreateAlert(DateTime triggerTime, WeatherTypes weatherLookout, INotificationSenderD notificationSender)
        {
            AlertD alert = new AlertD(triggerTime, weatherLookout, notificationSender);
            _alerts.Add(alert);
            return alert;
        }

        public void TriggerAlert(Guid id)
        {
            AlertD alert = _alerts.FirstOrDefault<AlertD>(alert => alert.Id == id);
            alert.Trigger();
        }

        public void AddNotificationMethod(Guid id, INotificationSenderD notificationSender)
        {
            AlertD alert = _alerts.FirstOrDefault<AlertD>(alert => alert.Id == id);
            alert.AlertDelegate = new AlertD.ExecuteAlertDelegate(notificationSender.SendNotification);
        }
    }

    public class AlertD
    {
        public Guid Id { get; set; }
        public DateTime TriggerTime { get; set; }
        public WeatherTypes WeatherLookout { get; set; }
        public INotificationSenderD NotificationSender { get; set; }

        public delegate void ExecuteAlertDelegate();
        private ExecuteAlertDelegate _alertDelegate;
        public ExecuteAlertDelegate AlertDelegate { set { _alertDelegate += value; } }

        public AlertD(DateTime triggerTime, WeatherTypes weatherLookout, INotificationSenderD notificationSender)
        {
            Id = Guid.NewGuid();
            TriggerTime = triggerTime;
            WeatherLookout = weatherLookout;
            NotificationSender = notificationSender;

            AlertDelegate = new ExecuteAlertDelegate(RunAlertHandler);
            AlertDelegate = new ExecuteAlertDelegate(NotificationSender.SendNotification);
        }
        public void RunAlertHandler()
        {
            Console.WriteLine($"Getting Weather Forecast...");
        }
        public void Trigger()
        {
            _alertDelegate();
        }
    }

    public interface INotificationSenderD
    {
        void SendNotification();
    }
    public class EmailNotificationsSenderD : INotificationSenderD
    {
        public void SendNotification()
        {
            Console.WriteLine("Sending Email Notification");
        }
    }
    public class SMSNotificationsSenderD : INotificationSenderD
    {
        public void SendNotification()
        {
            Console.WriteLine("Sending SMS Notification");
        }
    }
}
