﻿using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [Produces("application/json")]
    public class SubscribersController : BaseController
    {
        private readonly IRepository<Subscriber> _repository;

        public SubscribersController(IRepository<Subscriber> repository) =>
            _repository = repository;

        /// <summary>
        /// Get the enumeration of subscribers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /subscribers
        /// </remarks>
        /// <returns>Returns IEnumerable<SubscriberDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SubscriberDTO>>> GetAll() =>
            Ok((await _repository.GetAllAsync()).ToDTO());

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
        /// <response code="404">Not Found</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            await _repository.ExistsAsync(id) ? Ok(true) : NotFound(false);

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
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriberDTO>> Get(int id)
        {
            var subscriber = await _repository.GetAsync(id);
            if (subscriber is null) return NotFound();

            return Ok(subscriber.ToDTO());
        }

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
        /// <param name="subscriberDTO">SubscriberDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect subscriber DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] SubscriberDTO subscriberDTO)
        {
            var subscriber = await _repository.CreateAsync(subscriberDTO.FromDTO());
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
        /// <param name="subscriberDTO">SubscriberDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] SubscriberDTO subscriberDTO)
        {
            await _repository.UpdateAsync(subscriberDTO.FromDTO());
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
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            await _repository.DeleteAsync(id) ? NoContent() : NotFound();
    }
}