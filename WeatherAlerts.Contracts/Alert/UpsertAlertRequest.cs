using System;
using System.Text.Json.Serialization;

namespace WeatherAlerts.Contracts.Alert
{
    public class UpsertAlertRequest
    {
        public string Name { get; set; }
        public DateTime TriggerTime { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WeatherTypes WeatherLookout { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Locations LocationLookout { get; set; }
    }
}