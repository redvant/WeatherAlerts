using System;

namespace WeatherAlerts.EventHandler.Schedules
{
    public class AlertsEventArgs : EventArgs
    {
        DateTime TriggerTime { get; set; }

        public AlertsEventArgs(DateTime triggerTime)
        {
            TriggerTime = triggerTime;
        }
    }
}