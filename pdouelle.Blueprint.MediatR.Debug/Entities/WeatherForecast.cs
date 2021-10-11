using System;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.CreateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.DeleteWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.PatchWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.UpdateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastById;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastList;
using pdouelle.Blueprints.MediatR;
using pdouelle.Blueprints.MediatR.Attributes;
using pdouelle.Entity;

namespace pdouelle.Blueprint.MediatR.Debug.Entities
{
    [
        ApiResource
        (
            QueryList = typeof(GetWeatherForecastListQueryModel),
            QueryById = typeof(GetWeatherForecastByIdQueryModel),
            Create = typeof(CreateWeatherForecastCommandModel),
            Update = typeof(UpdateWeatherForecastCommandModel),
            Patch = typeof(PatchWeatherForecastCommandModel),
            Delete = typeof(DeleteWeatherForecastCommandModel)
        )
    ]
    public class WeatherForecast : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}