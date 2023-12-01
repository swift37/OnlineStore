using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.ContactRequest;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
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
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            Ok(await _repository.ExistsAsync(id));

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
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ContactRequestDTO>> Get(int id) =>
            Ok((await _repository.GetAsync(id)).ToDTO());

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
        /// <param name="createContactRequestDTO">CreateContactRequestDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect contactRequest DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CreateContactRequestDTO createContactRequestDTO)
        {
            var contactRequest = await _repository.CreateAsync(createContactRequestDTO.FromDTO());
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
        /// <param name="updateContactRequestDTO">UpdateContactRequestDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateContactRequestDTO updateContactRequestDTO)
        {
            await _repository.UpdateAsync(updateContactRequestDTO.FromDTO());
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
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
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
