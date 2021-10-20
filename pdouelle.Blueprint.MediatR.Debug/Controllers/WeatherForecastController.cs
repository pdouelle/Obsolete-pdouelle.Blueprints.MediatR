using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pdouelle.Blueprint.MediatR.Debug.Entities;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.CreateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.PatchWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastList;
using pdouelle.Blueprints.MediatR.Models.Commands.Create;
using pdouelle.Blueprints.MediatR.Models.Commands.Delete;
using pdouelle.Blueprints.MediatR.Models.Commands.Save;
using pdouelle.Blueprints.MediatR.Models.Commands.Update;
using pdouelle.Blueprints.MediatR.Models.Queries.IdQuery;
using pdouelle.Blueprints.MediatR.Models.Queries.ListQuery;
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
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            WeatherForecast response = await _mediator.Send(new IdQueryModel<WeatherForecast>
            {
                Id = id
            }, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWeatherForecastCommandModel parameters, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<WeatherForecast>(parameters);
            
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
    }
}