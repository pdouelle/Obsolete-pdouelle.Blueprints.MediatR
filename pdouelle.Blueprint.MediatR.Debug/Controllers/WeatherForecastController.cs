using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Commands.CreateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Commands.PatchWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Queries.GetWeatherForecastList;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Models.Queries.GetWeatherForecastSingle;
using pdouelle.Blueprints.MediatR.Models.Commands.Create;
using pdouelle.Blueprints.MediatR.Models.Commands.Delete;
using pdouelle.Blueprints.MediatR.Models.Commands.Save;
using pdouelle.Blueprints.MediatR.Models.Commands.Update;
using pdouelle.Blueprints.MediatR.Models.Queries.ExistsQuery;
using pdouelle.Blueprints.MediatR.Models.Queries.IdQuery;
using pdouelle.Blueprints.MediatR.Models.Queries.ListQuery;
using pdouelle.Blueprints.MediatR.Models.Queries.SingleQuery;
using pdouelle.Pagination;

namespace pdouelle.Blueprint.MediatR.Debug.Controllers
{
    [ApiController]
    [Route("WeatherForecast")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WeatherForecastController(IMediator mediator, IMapper mapper)
        {
            Guard.Against.Null(mediator, nameof(mediator));
            Guard.Against.Null(mapper, nameof(mapper));

            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetWeatherForecastListQueryModel request, CancellationToken cancellationToken)
        {
            PagedList<WeatherForecast> response = await _mediator.Send(new ListQueryModel<WeatherForecast, GetWeatherForecastListQueryModel>
            {
                Request = request
            }, cancellationToken);
            
            var metadata = new
            {
               response.TotalCount,
               response.PageSize,
               response.CurrentPage,
               response.TotalPages,
               response.HasNext,
               response.HasPrevious
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromQuery] GetWeatherForecastSingleQueryModel request, CancellationToken cancellationToken)
        {
            WeatherForecast response = await _mediator.Send(new SingleQueryModel<WeatherForecast, GetWeatherForecastSingleQueryModel>
            {
                Request = request
            }, cancellationToken);
            
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWeatherForecastCommandModel model, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<WeatherForecast>(model);
            
            WeatherForecast entity = await _mediator.Send(new CreateCommandModel<WeatherForecast>
            { 
                Entity = request
            }, cancellationToken);

            await _mediator.Send(new SaveCommandModel<WeatherForecast>(), cancellationToken);

            return Created($"{HttpContext.Request.Path}/{entity.Id}", entity);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(Guid id, [FromBody] JsonPatchDocument<PatchWeatherForecastCommandModel> entityPatch, CancellationToken cancellationToken)
        {
            WeatherForecast entity = await _mediator.Send(new IdQueryModel<WeatherForecast>
            {
                Id = id
            }, cancellationToken);

            if (entity == null)
                return NotFound();

            var entityCopy = _mapper.Map<PatchWeatherForecastCommandModel>(entity);
            
            entityPatch.ApplyTo(entityCopy);
            
            _mapper.Map(entityCopy, entity);
            
            await _mediator.Send(new UpdateCommandModel<WeatherForecast>
            {
                Entity = entity,
            }, cancellationToken);

            await _mediator.Send(new SaveCommandModel<WeatherForecast>(), cancellationToken);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            WeatherForecast entity = await _mediator.Send(new IdQueryModel<WeatherForecast>
            {
                Id = id
            }, cancellationToken);

            if (entity == null)
                return NotFound();

            await _mediator.Send(new DeleteCommandModel<WeatherForecast>
            {
                Entity = entity,
            }, cancellationToken);

            await _mediator.Send(new SaveCommandModel<WeatherForecast>(), cancellationToken);

            return NoContent();
        }
        
        [HttpGet("exists/TemperatureC/{temperatureC:int}")]
        public async Task<IActionResult> ExistsAsync(int temperatureC, CancellationToken cancellationToken)
        {
            // [Exists(Resource = typeof(WeatherForecast), Name = optional)
            // new ExistsQueryModel<WeatherForecast>(propertyName, value)
            // new ExistsQueryModel<WeatherForecast>(List<propertyName, value>)
            
            var existRequest = new List<KeyValuePair<string, object>>
            {
                new(nameof(WeatherForecast.TemperatureC), temperatureC)
            };
            
            var exist = await _mediator.Send(new ExistsQueryModel<WeatherForecast>(existRequest), cancellationToken);
            
            return Ok(exist);
        }
    }
}