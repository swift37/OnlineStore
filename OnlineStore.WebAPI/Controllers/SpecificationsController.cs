using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Specification;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;
using AutoMapper;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class SpecificationsController : BaseController
    {
        private readonly ISpecificationsRepository _repository;

        private readonly IMapper _mapper;

        public SpecificationsController(ISpecificationsRepository repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of specifications
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specifications
        /// </remarks>
        /// <returns>Returns IEnumerable<SpecificationDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<SpecificationDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<SpecificationDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if specification exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specifications/exists/1
        /// </remarks>
        /// <param name="id">Specification id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            Ok(await _repository.ExistsAsync(id));

        /// <summary>
        /// Get the specification by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specifications/1
        /// </remarks>
        /// <param name="id">Specification id (int)</param>
        /// <returns>Returns SpecificationDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<SpecificationDTO>> Get(int id) =>
            Ok(_mapper.Map<SpecificationDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a specification
        /// </summary>
        /// <remarks>
        /// POST /specifications
        /// {
        ///     value: "Specification value",
        /// }
        /// </remarks>
        /// <param name="createSpecificationDTO">CreateSpecificationDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect specification DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateSpecificationDTO createSpecificationDTO)
        {
            var specification = await _repository.CreateAsync(_mapper.Map<Specification>(createSpecificationDTO));
            if (specification is null) return UnprocessableEntity();
            return Ok(specification.Id);
        }

        /// <summary>
        /// Partially update the specification
        /// </summary>
        /// <remarks>
        /// PATCH /specifications
        /// {
        ///     id: "1",
        ///     value: "Updated specification value"
        /// }
        /// </remarks>
        /// <param name="updateSpecificationDTO">UpdateSpecificationDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateSpecificationDTO updateSpecificationDTO)
        {
            var specification = await _repository.GetAsync(updateSpecificationDTO.Id);
            specification.Value = updateSpecificationDTO.Value;
            specification.SpecificationTypeId = updateSpecificationDTO.SpecificationTypeId;
            
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the specification by id
        /// </summary>
        /// <remarks>
        /// DELETE /specifications/1
        /// </remarks>
        /// <param name="id">Specification id (int)</param>
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
        /// Get the enumeration of specifications by ids
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specifications/many?ids=1&ids=27&ids=51&ids=89
        /// </remarks>
        /// <param name="ids">Specification ids (int[])</param>
        /// <returns>Returns IEnumerable<SpecificationDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet("many")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpecificationDTO>>> GetMany([FromQuery] IEnumerable<int> ids) =>
            Ok(_mapper.Map<IEnumerable<SpecificationDTO>>(await _repository.GetManyAsync(ids)));
    }
}
