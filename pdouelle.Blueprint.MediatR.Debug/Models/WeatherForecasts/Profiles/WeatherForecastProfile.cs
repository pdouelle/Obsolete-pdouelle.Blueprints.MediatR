using AutoMapper;
using pdouelle.Blueprint.MediatR.Debug.Entities;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.CreateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.PatchWeatherForecast;

namespace pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Profiles
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<CreateWeatherForecastCommandModel, WeatherForecast>();
            CreateMap<WeatherForecast, PatchWeatherForecastCommandModel>();
            CreateMap<PatchWeatherForecastCommandModel, WeatherForecast>();
        }
    }
}