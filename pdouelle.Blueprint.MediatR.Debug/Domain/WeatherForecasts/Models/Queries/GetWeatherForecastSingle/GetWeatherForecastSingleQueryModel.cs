using System;
using Microsoft.AspNetCore.Mvc;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;
using pdouelle.LinqExtensions.Attributes;
using pdouelle.Sort;

namespace pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Queries.GetWeatherForecastSingle
{
    public class GetWeatherForecastSingleQueryModel : ISort
    {
        [Where(Name = nameof(WeatherForecast.Id))]
        [FromRoute]
        public Guid Id { get; set; }
        
        [Include(Name = nameof(WeatherForecast.ChildEntity))]
        public bool IncludeChildEntities { get; set; }

        public string Sort { get; set; }
    }
}