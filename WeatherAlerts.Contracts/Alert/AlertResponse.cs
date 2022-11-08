using System;
using System.Text.Json.Serialization;

namespace WeatherAlerts.Contracts.Alert
{
    public class AlertResponse
    {
        public AlertResponse(Guid id, string name, DateTime triggerTime, DateTime lastModifiedDateTime, WeatherTypes weatherLookout, Locations locationLookout)
        {
            Id = id;
            Name = name;
            TriggerTime = triggerTime;
            LastModifiedDateTime = lastModifiedDateTime;
            WeatherLookout = weatherLookout;
            LocationLookout = locationLookout;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TriggerTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WeatherTypes WeatherLookout { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Locations LocationLookout { get; set; }
    }
}