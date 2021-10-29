using System.Collections.Generic;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;
using pdouelle.LinqExtensions.Interfaces;
using pdouelle.QueryStringParameters;

namespace pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Queries.GetWeatherForecastList
{
    public class GetWeatherForecastListQueryModel : QueryStringPaginationSort, IInclude
    {
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

        
        public List<string> Include { get; set; }
    }
}