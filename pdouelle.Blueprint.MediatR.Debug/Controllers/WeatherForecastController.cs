using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pdouelle.Blueprint.MediatR.Debug.Entities;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.CreateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.DeleteWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.PatchWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Commands.UpdateWeatherForecast;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastById;
using pdouelle.Blueprint.MediatR.Debug.Models.WeatherForecasts.Models.Queries.GetWeatherForecastList;
using pdouelle.Blueprints.MediatR.Models.Commands.Create;
using pdouelle.Blueprints.MediatR.Models.Commands.Delete;
using pdouelle.Blueprints.MediatR.Models.Commands.Patch;
using pdouelle.Blueprints.MediatR.Models.Commands.Save;
using pdouelle.Blueprints.MediatR.Models.Commands.Update;
using pdouelle.Blueprints.MediatR.Models.Queries.IdQuery;
using pdouelle.Blueprints.MediatR.Models.Queries.ListQuery;
using pdouelle.Pagination;

namespace pdouelle.Blueprint.MediatR.Debug.Controllers
{
    [ApiController]
    [Route("api/WeatherForecast")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherForecastsAsync([FromQuery] GetWeatherForecastListQueryModel request)
        {
            PagedList<WeatherForecast> response = await _mediator.Send(
                new ListQueryModel<WeatherForecast, GetWeatherForecastListQueryModel>
                {
                    Request = request
                });
            
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

        [HttpGet("{id}", Name = nameof(GetWeatherForecastsByIdAsync))]
        public async Task<IActionResult> GetWeatherForecastsByIdAsync(Guid id)
        {
            WeatherForecast response = await _mediator.Send(
                new IdQueryModel<WeatherForecast, GetWeatherForecastByIdQueryModel>
                {
                    Request = new GetWeatherForecastByIdQueryModel {Id = id}
                });

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeatherForecastsAsync(
            [FromBody] CreateWeatherForecastCommandModel request)
        {
            WeatherForecast entity = await _mediator.Send(
                new CreateCommandModel<WeatherForecast, CreateWeatherForecastCommandModel>
                {
                    Request = request
                });

            await _mediator.Send(new SaveCommandModel<WeatherForecast>());

            return CreatedAtRoute(nameof(GetWeatherForecastsByIdAsync), new {id = entity.Id}, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeatherForecastsAsync
            (Guid id, [FromBody] UpdateWeatherForecastCommandModel request)
        {
            WeatherForecast entity = await _mediator.Send(
                new IdQueryModel<WeatherForecast, GetWeatherForecastByIdQueryModel>
                {
                    Request = new GetWeatherForecastByIdQueryModel {Id = id}
                });

            if (entity == null)
                return NotFound();

            await _mediator.Send(new UpdateCommandModel<WeatherForecast, UpdateWeatherForecastCommandModel>
            {
                Entity = entity,
                Request = request
            });

            await _mediator.Send(new SaveCommandModel<WeatherForecast>());

            return Ok(entity);
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchWeatherForecastsAsync
            (Guid id, [FromBody] JsonPatchDocument<PatchWeatherForecastCommandModel> patchDocument)
        {
            WeatherForecast entity = await _mediator.Send(
                new IdQueryModel<WeatherForecast, GetWeatherForecastByIdQueryModel>
                {
                    Request = new GetWeatherForecastByIdQueryModel {Id = id}
                });

            if (entity == null)
                return NotFound();
            
            var request = new PatchWeatherForecastCommandModel();
            patchDocument.ApplyTo(request);

            await _mediator.Send(new PatchCommandModel<WeatherForecast, PatchWeatherForecastCommandModel>
            {
                Entity = entity,
                Request = request
            });

            await _mediator.Send(new SaveCommandModel<WeatherForecast>());

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationTemplatesAsync(Guid id, [FromQuery]DeleteWeatherForecastCommandModel request)
        {
            WeatherForecast entity = await _mediator.Send(
                new IdQueryModel<WeatherForecast, GetWeatherForecastByIdQueryModel>
                {
                    Request = new GetWeatherForecastByIdQueryModel {Id = id}
                });

            if (entity == null)
                return NotFound();

            await _mediator.Send(new DeleteCommandModel<WeatherForecast, DeleteWeatherForecastCommandModel>
            {
                Entity = entity,
                Request = request
            });

            await _mediator.Send(new SaveCommandModel<WeatherForecast>());

            return NoContent();
        }
    }
}