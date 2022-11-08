using Microsoft.AspNetCore.Mvc;
using System;
using WeatherAlerts.Api.Models;
using WeatherAlerts.Api.Services.Alerts;
using WeatherAlerts.Contracts.Alert;

namespace WeatherAlerts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpPost]
        public IActionResult CreateAlert([FromBody] CreateAlertRequest request)
        {
            var alert = new Alert(
                Guid.NewGuid(),
                request.Name,
                request.TriggerTime,
                DateTime.UtcNow,
                request.WeatherLookout,
                request.LocationLookout);

            _alertService.CreateAlert(alert);

            var response = new AlertResponse(
                alert.Id,
                alert.Name,
                alert.TriggerTime,
                alert.LastModifiedDateTime,
                alert.WeatherLookout,
                alert.LocationLookout);

            return CreatedAtAction(
                actionName: nameof(CreateAlert),
                routeValues: new { id = alert.Id },
                value: response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetAlert(Guid id)
        {
            Alert alert = _alertService.GetAlert(id);

            var response = new AlertResponse(
                alert.Id,
                alert.Name,
                alert.TriggerTime,
                alert.LastModifiedDateTime,
                alert.WeatherLookout,
                alert.LocationLookout);

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpsertAlert(Guid id, [FromBody] UpsertAlertRequest request)
        {
            var alert = new Alert(
                id,
                request.Name,
                request.TriggerTime,
                DateTime.UtcNow,
                request.WeatherLookout,
                request.LocationLookout);

            _alertService.UpsertAlert(alert);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteAlert(Guid id)
        {
            _alertService.DeleteAlert(id);
            return NoContent();
        }
    }
}
