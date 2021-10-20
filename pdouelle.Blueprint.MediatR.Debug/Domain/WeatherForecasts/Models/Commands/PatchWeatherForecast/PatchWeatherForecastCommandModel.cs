using System;

namespace pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Commands.PatchWeatherForecast
{
    public class PatchWeatherForecastCommandModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}