using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Subscriber;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class SubscribersController : BaseController
    {
        private readonly ISubscribersRepository _repository;

        private readonly IMapper _mapper;

        public SubscribersController(ISubscribersRepository repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of subscribers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /subscribers
        /// </remarks>
        /// <returns>Returns IEnumerable<SubscriberDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<SubscriberDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<SubscriberDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if subscriber exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /subscribers/exists/1
        /// </remarks>
        /// <param name="id">Subscriber id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
        Ok(await _repository.ExistsAsync(id));

        /// <summary>
        /// Get the subscriber by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /subscribers/1
        /// </remarks>
        /// <param name="id">Subscriber id (int)</param>
        /// <returns>Returns SubscriberDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<SubscriberDTO>> Get(int id) => 
            Ok(_mapper.Map<SubscriberDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a subscriber
        /// </summary>
        /// <remarks>
        /// POST /subscribers
        /// {
        ///     name: "Subscriber name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createSubscriberDTO">CreateSubscriberDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect subscriber DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CreateSubscriberDTO createSubscriberDTO)
        {
            var subscriber = await _repository.CreateAsync(_mapper.Map<Subscriber>(createSubscriberDTO));
            if (subscriber is null) return UnprocessableEntity();
            return Ok(subscriber.Id);
        }

        /// <summary>
        /// Update the subscriber
        /// </summary>
        /// <remarks>
        /// PUT /subscribers
        /// {
        ///     name: "Updated subscriber name"
        /// }
        /// </remarks>
        /// <param name="updateSubscriberDTO">UpdateSubscriberDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateSubscriberDTO updateSubscriberDTO)
        {
            await _repository.UpdateAsync(_mapper.Map<Subscriber>(updateSubscriberDTO));
            return NoContent();
        }

        /// <summary>
        /// Delete the subscriber by id
        /// </summary>
        /// <remarks>
        /// DELETE /subscribers/1
        /// </remarks>
        /// <param name="id">Subscriber id (int)</param>
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

        /// <summary>
        /// Get the subscriber by email
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /subscribers/onlinestore@gmail.com
        /// </remarks>
        /// <param name="email">Subscriber email</param>
        /// <returns>Returns SubscriberDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{email}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<SubscriberDTO>> Get(string email) =>
            Ok(_mapper.Map<SubscriberDTO>(await _repository.GetAsync(email)));
    }
}
