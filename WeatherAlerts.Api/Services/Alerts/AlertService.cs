using System;
using System.Collections.Generic;
using WeatherAlerts.Api.Models;

namespace WeatherAlerts.Api.Services.Alerts
{
    public class AlertService : IAlertService
    {
        private static readonly Dictionary<Guid, Alert> _alerts = new Dictionary<Guid, Alert>();
        public void CreateAlert(Alert alert)
        {
            _alerts.Add(alert.Id, alert);
        }

        public void DeleteAlert(Guid id)
        {
            _alerts.Remove(id);
        }

        public Alert GetAlert(Guid id)
        {
            return _alerts[id];
        }

        public void UpsertAlert(Alert alert)
        {
            _alerts[alert.Id] = alert;
        }
    }
}
