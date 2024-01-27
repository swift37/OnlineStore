using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.ContactRequest;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class ContactRequestsController : BaseController
    {
        private readonly IRepository<ContactRequest> _repository;

        private readonly IMapper _mapper;

        public ContactRequestsController(IRepository<ContactRequest> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

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
            Ok(_mapper.Map<IEnumerable<ContactRequestDTO>>(await _repository.GetAllAsync()));

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
            Ok(_mapper.Map<ContactRequestDTO>(await _repository.GetAsync(id)));

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
            var contactRequest = await _repository.CreateAsync(_mapper.Map<ContactRequest>(createContactRequestDTO));
            if (contactRequest is null) return UnprocessableEntity();
            return Ok(contactRequest.Id);
        }

        /// <summary>
        /// Full update the contact request
        /// </summary>
        /// <remarks>
        /// PUT /contactrequests
        /// {
        ///     id: "1",
        ///     name: "Updated contact request name"
        /// }
        /// </remarks>
        /// <param name="contactRequestDTO">ContactRequestDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] ContactRequestDTO contactRequestDTO)
        {
            await _repository.UpdateAsync(_mapper.Map<ContactRequest>(contactRequestDTO));
            return NoContent();
        }

        /// <summary>
        /// Partially update the contact request
        /// </summary>
        /// <remarks>
        /// PATCH /contactrequests
        /// {
        ///     id: "1",
        ///     name: "Updated contact request name"
        /// }
        /// </remarks>
        /// <param name="updateContactRequestDTO">UpdateContactRequestDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateContactRequestDTO updateContactRequestDTO)
        {
            var contactRequest = await _repository.GetAsync(updateContactRequestDTO.Id);
            contactRequest.ResponseDate = DateTime.Now;
            contactRequest.ContactName = contactRequest.ContactName;
            contactRequest.Email = contactRequest.Email;
            contactRequest.Message = contactRequest.Message;

            await _repository.SaveChangesAsync();

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
