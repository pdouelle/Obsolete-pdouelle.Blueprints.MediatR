using AutoMapper;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.CreateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.PatchWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.UpdateWeatherForecast;

namespace pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Profiles
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<CreateWeatherForecastCommandModel, Entities.WeatherForecast>();
            CreateMap<UpdateWeatherForecastCommandModel, Entities.WeatherForecast>();
            CreateMap<PatchWeatherForecastCommandModel, Entities.WeatherForecast>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}