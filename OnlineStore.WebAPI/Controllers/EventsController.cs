using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Event;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class EventsController : BaseController
    {
        private readonly IRepository<Event> _repository;

        private readonly IMapper _mapper;

        public EventsController(IRepository<Event> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of events
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /events
        /// </remarks>
        /// <returns>Returns IEnumerable<EventDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<EventDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if event exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /events/exists/1
        /// </remarks>
        /// <param name="id">Event id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            Ok(await _repository.ExistsAsync(id));

        /// <summary>
        /// Get the event by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /events/1
        /// </remarks>
        /// <param name="id">Event id (int)</param>
        /// <returns>Returns EventDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventDTO>> Get(int id) => 
            Ok(_mapper.Map<EventDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a event
        /// </summary>
        /// <remarks>
        /// POST /events
        /// {
        ///     name: "Event name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createEventDTO">CreateEventDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect event DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateEventDTO createEventDTO)
        {
            var @event = await _repository.CreateAsync(_mapper.Map<Event>(createEventDTO));
            if (@event is null) return UnprocessableEntity();
            return Ok(@event.Id);
        }

        /// <summary>
        /// Full update the event
        /// </summary>
        /// <remarks>
        /// PUT /events
        /// {
        ///     id: "1",
        ///     name: "Updated event name"
        /// }
        /// </remarks>
        /// <param name="eventDTO">EventDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] EventDTO eventDTO)
        {
            await _repository.UpdateAsync(_mapper.Map<Event>(eventDTO));
            return NoContent();
        }

        /// <summary>
        /// Partially update the event
        /// </summary>
        /// <remarks>
        /// PATCH /events
        /// {
        ///     id: "1",
        ///     name: "Updated event name"
        /// }
        /// </remarks>
        /// <param name="updateEventDTO">UpdateEventDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateEventDTO updateEventDTO)
        {
            var @event = await _repository.GetAsync(updateEventDTO.Id);
            @event.Name = updateEventDTO.Name;
            @event.Image = updateEventDTO.Image;
            @event.Description = updateEventDTO.Description;
            @event.StartDate = updateEventDTO.StartDate;
            @event.FinishDate = updateEventDTO.FinishDate;

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the event by id
        /// </summary>
        /// <remarks>
        /// DELETE /events/1
        /// </remarks>
        /// <param name="id">Event id (int)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
