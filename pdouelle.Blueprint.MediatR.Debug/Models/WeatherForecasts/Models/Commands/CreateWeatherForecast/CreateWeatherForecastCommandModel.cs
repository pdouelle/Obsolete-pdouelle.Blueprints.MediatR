using System;

namespace pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.CreateWeatherForecast
{
    public class CreateWeatherForecastCommandModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}