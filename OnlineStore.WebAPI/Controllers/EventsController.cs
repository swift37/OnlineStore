using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Event;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [Produces("application/json")]
    public class EventsController : BaseController
    {
        private readonly IRepository<Event> _repository;

        public EventsController(IRepository<Event> repository) =>
            _repository = repository;

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
            Ok((await _repository.GetAllAsync()).ToDTO());

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
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            Ok((await _repository.GetAsync(id)).ToDTO());

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
        /// <param name="eventDTO">EventDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect event DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] EventDTO eventDTO)
        {
            var @event = await _repository.CreateAsync(eventDTO.FromDTO());
            if (@event is null) return UnprocessableEntity();
            return Ok(@event.Id);
        }

        /// <summary>
        /// Update the event
        /// </summary>
        /// <remarks>
        /// PUT /events
        /// {
        ///     name: "Updated event name"
        /// }
        /// </remarks>
        /// <param name="eventDTO">EventDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] EventDTO eventDTO)
        {
            await _repository.UpdateAsync(eventDTO.FromDTO());
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
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
