using System;
using WeatherAlerts.Api.Models;

namespace WeatherAlerts.Api.Services.Alerts
{
    public interface IAlertService
    {
        void CreateAlert(Alert request);
        void DeleteAlert(Guid id);
        Alert GetAlert(Guid id);
        void UpsertAlert(Alert alert);
    }
}
