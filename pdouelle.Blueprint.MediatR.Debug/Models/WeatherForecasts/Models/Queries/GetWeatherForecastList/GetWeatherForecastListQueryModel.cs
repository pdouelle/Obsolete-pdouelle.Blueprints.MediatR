using pdouelle.QueryStringParameters;

namespace pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastList
{
    public class GetWeatherForecastListQueryModel : QueryStringPaginationSort
    {
        public bool IncludeBlobs { get; set; }
    }
}