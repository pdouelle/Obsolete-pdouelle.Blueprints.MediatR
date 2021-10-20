using System;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastList;
using pdouelle.Blueprints.MediatR.Attributes;
using pdouelle.Entity;

namespace pdouelle.Blueprint.MediatR.Debug.Entities
{
    [
        ApiResource
        (
            QueryList = typeof(GetWeatherForecastListQueryModel)
        )
    ]
    public class WeatherForecast : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}