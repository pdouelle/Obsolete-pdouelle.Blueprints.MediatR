using System;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;
using pdouelle.Blueprints.MediatR.Attributes;
using pdouelle.Entity;

namespace pdouelle.Blueprint.MediatR.Debug.Domain.ChildEntities.Entities
{
    [ApiResource]
    public class ChildEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; }
    }
}