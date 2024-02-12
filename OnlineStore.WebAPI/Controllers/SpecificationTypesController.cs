using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.SpecificationType;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;
using AutoMapper;
using OnlineStore.Domain;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/specification-types")]
    public class SpecificationTypesController : BaseController
    {
        private readonly ISpecificationTypesRepository _repository;

        private readonly IMapper _mapper;

        public SpecificationTypesController(ISpecificationTypesRepository repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of specification types
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specification-types
        /// </remarks>
        /// <returns>Returns IEnumerable<SpecificationTypeDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpecificationTypeDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<SpecificationTypeDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if specification type exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specification-types/exists/1
        /// </remarks>
        /// <param name="id">SpecificationType id</param>
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
        /// Get the specification type by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specification-types/1
        /// </remarks>
        /// <param name="id">SpecificationType id (int)</param>
        /// <returns>Returns SpecificationTypeDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecificationTypeDTO>> Get(int id) =>
            Ok(_mapper.Map<SpecificationTypeDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a specification type
        /// </summary>
        /// <remarks>
        /// POST /specification-types
        /// {
        ///     name: "SpecificationType name"
        /// }
        /// </remarks>
        /// <param name="createSpecificationTypeDTO">CreateSpecificationTypeDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect specification type DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateSpecificationTypeDTO createSpecificationTypeDTO)
        {
            var specificationType = await _repository.CreateAsync(_mapper.Map<SpecificationType>(createSpecificationTypeDTO));
            if (specificationType is null) return UnprocessableEntity();
            return Ok(specificationType.Id);
        }

        /// <summary>
        /// Partially update the specification type
        /// </summary>
        /// <remarks>
        /// PATCH /specification-types
        /// {
        ///     id: "1",
        ///     name: "Updated specification type name"
        /// }
        /// </remarks>
        /// <param name="updateSpecificationTypeDTO">UpdateSpecificationTypeDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateSpecificationTypeDTO updateSpecificationTypeDTO)
        {
            var specificationType = await _repository.GetAsync(updateSpecificationTypeDTO.Id);
            specificationType.Name = updateSpecificationTypeDTO.Name;
            specificationType.DisplayName = updateSpecificationTypeDTO.DisplayName;
            specificationType.IsMain = updateSpecificationTypeDTO.IsMain;

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the specification type by id
        /// </summary>
        /// <remarks>
        /// DELETE /specification-types/1
        /// </remarks>
        /// <param name="id">SpecificationType id (int)</param>
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
        /// Get the specification type by id options
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /specification-types/by-options/
        /// </remarks>
        /// <param name="specificationTypeOptionsDTO">SpecificationTypeOptionsDTO</param>
        /// <returns>Returns SpecificationTypeDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet("by-options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SpecificationTypeDTO>> Get(
            [FromBody] SpecificationTypeOptionsDTO specificationTypeOptionsDTO)
        {
            var specificationTypeOptions = _mapper.Map<SpecificationTypeOptions>(specificationTypeOptionsDTO);
            var specificationType =
                await _repository.GetAsync(specificationTypeOptions);

            return Ok(_mapper.Map<SpecificationTypeDTO>(specificationType));
        }
    }
}
