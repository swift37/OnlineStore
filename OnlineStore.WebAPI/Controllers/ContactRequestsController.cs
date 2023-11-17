using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [Produces("application/json")]
    public class ContactRequestsController : BaseController
    {
        private readonly IRepository<ContactRequest> _repository;

        public ContactRequestsController(IRepository<ContactRequest> repository) =>
            _repository = repository;

        /// <summary>
        /// Get the enumeration of contact requests
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /contactrequests
        /// </remarks>
        /// <returns>Returns IEnumerable<ContactRequestDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ContactRequestDTO>>> GetAll() =>
            Ok((await _repository.GetAllAsync()).ToDTO());

        /// <summary>
        /// Get true if contact request exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /contactrequests/exists/1
        /// </remarks>
        /// <param name="id">ContactRequest id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            await _repository.ExistsAsync(id) ? Ok(true) : NotFound(false);

        /// <summary>
        /// Get the contact request by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /contactrequests/1
        /// </remarks>
        /// <param name="id">ContactRequest id (int)</param>
        /// <returns>Returns ContactRequestDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactRequestDTO>> Get(int id)
        {
            var contactRequest = await _repository.GetAsync(id);
            if (contactRequest is null) return NotFound();

            return Ok(contactRequest.ToDTO());
        }

        /// <summary>
        /// Create a contact request
        /// </summary>
        /// <remarks>
        /// POST /contactrequests
        /// {
        ///     name: "ContactRequest name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="contactRequestDTO">ContactRequestDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect contactRequest DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] ContactRequestDTO contactRequestDTO)
        {
            var contactRequest = await _repository.CreateAsync(contactRequestDTO.FromDTO());
            if (contactRequest is null) return UnprocessableEntity();
            return Ok(contactRequest.Id);
        }

        /// <summary>
        /// Update the contact request
        /// </summary>
        /// <remarks>
        /// PUT /contactrequests
        /// {
        ///     name: "Updated contact request name"
        /// }
        /// </remarks>
        /// <param name="contactRequestDTO">ContactRequestDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] ContactRequestDTO contactRequestDTO)
        {
            await _repository.UpdateAsync(contactRequestDTO.FromDTO());
            return NoContent();
        }

        /// <summary>
        /// Delete the contact request by id
        /// </summary>
        /// <remarks>
        /// DELETE /contactrequests/1
        /// </remarks>
        /// <param name="id">ContactRequest id (int)</param>
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
