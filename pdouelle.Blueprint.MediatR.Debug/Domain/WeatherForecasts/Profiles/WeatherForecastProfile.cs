using AutoMapper;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Commands.CreateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Commands.PatchWeatherForecast;

namespace pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Profiles
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