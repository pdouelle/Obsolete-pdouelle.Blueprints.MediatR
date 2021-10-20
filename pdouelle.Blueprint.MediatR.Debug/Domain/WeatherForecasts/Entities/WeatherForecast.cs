using System;
using pdouelle.Blueprint.MediatR.Debug.Domain.ChildEntities.Entities;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Queries.GetWeatherForecastList;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Queries.GetWeatherForecastSingle;
using pdouelle.Blueprints.MediatR.Attributes;
using pdouelle.Entity;

namespace pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities
{
    [
        ApiResource
        (
            QueryList = typeof(GetWeatherForecastListQueryModel),
            QuerySingle = typeof(GetWeatherForecastSingleQueryModel)
        )
    ]
    public class WeatherForecast : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }

        public virtual ChildEntity ChildEntity { get; set; }
    }
}