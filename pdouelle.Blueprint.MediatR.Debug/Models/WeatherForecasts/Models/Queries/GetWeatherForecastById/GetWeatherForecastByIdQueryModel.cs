using System;
using pdouelle.Entity;

namespace pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastById
{
    public class GetWeatherForecastByIdQueryModel : IEntity
    {
        public Guid Id { get; set; }
    }
}