using System.Text.Json.Serialization;
using System;
using WeatherAlerts.Contracts;

namespace WeatherAlerts.Api.Models
{
    public class Alert
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime TriggerTime { get; }
        public DateTime LastModifiedDateTime { get; }
        public WeatherTypes WeatherLookout { get; }
        public Locations LocationLookout { get; }

        public Alert(
            Guid id, 
            string name, 
            DateTime triggerTime, 
            DateTime lastModifiedDateTime, 
            WeatherTypes weatherLookout, 
            Locations location)
        {
            Id = id;
            Name = name;
            TriggerTime = triggerTime;
            LastModifiedDateTime = lastModifiedDateTime;
            WeatherLookout = weatherLookout;
            LocationLookout = location;
        }
    }
}
