using System;
using WeatherAlerts.EventHandler.Alerts;
using WeatherAlerts.EventHandler.Notifications;
using WeatherAlerts.EventHandler.Schedules;

namespace WeatherAlerts.EventHandler
{
    public static class EventHandlerExample
    {
        public static void Test()
        {
            EmailNotificationsSender emailSender = new EmailNotificationsSender("edgar@email.com");
            Schedule schedule = new Schedule();
            Alert alert = schedule.CreateAlert(DateTime.Now.AddSeconds(5), WeatherTypes.Rain, Locations.MexicoCity, emailSender);

            SMSNotificationsSender smsSender = new SMSNotificationsSender("555-5555");
            alert.AddNotificationMethod(smsSender);

            Alert alert2 = schedule.CreateAlert(DateTime.Now.AddSeconds(15), WeatherTypes.Snow, Locations.Monterrey, emailSender);

            schedule.RunSchedule();
        }
    }
}
