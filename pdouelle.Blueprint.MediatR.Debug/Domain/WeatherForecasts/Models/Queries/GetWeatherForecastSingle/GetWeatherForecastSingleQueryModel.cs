using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;
using pdouelle.LinqExtensions.Attributes;
using pdouelle.LinqExtensions.Interfaces;
using pdouelle.Sort;

namespace pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Queries.GetWeatherForecastSingle
{
    public class GetWeatherForecastSingleQueryModel : ISort, IInclude
    {
        [Where]
        [FromRoute]
        public Guid Id { get; set; }

        public bool IncludeChildEntities
        {
            set
            {
                if (value is true)
                {
                    Include.Add(nameof(WeatherForecast.ChildEntity));
                }
            }
        }

        public string Sort { get; set; }
        
        public List<string> Include { get; set; }
    }
}