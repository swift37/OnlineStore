using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.FiltersGroup;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.Persistence.Repositories;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/filter-groups")]
    public class FilterGroupsController : BaseController
    {
        private readonly IFilterGroupsRepository _filterGroupsRepository;
        private readonly IRepository<SpecificationType> _specificationTypesRepository;

        private readonly IMapper _mapper;

        public FilterGroupsController(
            IFilterGroupsRepository filterGroupsRepository,
            IRepository<SpecificationType> specificationTypesRepository,
            IMapper mapper) => 
            (_filterGroupsRepository, _specificationTypesRepository, _mapper) = 
            (filterGroupsRepository, specificationTypesRepository, mapper);

        /// <summary>
        /// Get the enumeration of filters groups
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /filtergroups
        /// </remarks>
        /// <returns>Returns IEnumerable<FiltersGroupDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FiltersGroupDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<FiltersGroupDTO>>(await _filterGroupsRepository.GetAllAsync()));

        /// <summary>
        /// Get true if filters group exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /filtergroups/exists/1
        /// </remarks>
        /// <param name="id">FiltersGroup id</param>
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
            Ok(await _filterGroupsRepository.ExistsAsync(id));

        /// <summary>
        /// Get the filters group by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /filtergroups/1
        /// </remarks>
        /// <param name="id">FiltersGroup id (int)</param>
        /// <returns>Returns FiltersGroupDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FiltersGroupDTO>> Get(int id) =>
            Ok(_mapper.Map<FiltersGroupDTO>(await _filterGroupsRepository.GetAsync(id)));

        /// <summary>
        /// Create a filters group
        /// </summary>
        /// <remarks>
        /// POST /filtergroups
        /// {
        ///     name: "FiltersGroup name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createFiltersGroupDTO">CreateFiltersGroupDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect filters group DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateFiltersGroupDTO createFiltersGroupDTO)
        {
            var filtersGroup = _mapper.Map<FiltersGroup>(createFiltersGroupDTO);

            foreach (var specificationTypeId in createFiltersGroupDTO.SpecificationTypeIds)
            {
                var specificationType = await _specificationTypesRepository.GetAsync(specificationTypeId);
                filtersGroup.SpecificationTypes.Add(specificationType);
            };

            if (await _filterGroupsRepository.CreateAsync(filtersGroup) is null)
                return UnprocessableEntity();

            return Ok(filtersGroup.Id);
        }

        /// <summary>
        /// Partially update the filters group
        /// </summary>
        /// <remarks>
        /// PATCH /filtergroups
        /// {
        ///     id: "1",
        ///     name: "Updated filters group name"
        /// }
        /// </remarks>
        /// <param name="updateFiltersGroupDTO">UpdateFiltersGroupDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateFiltersGroupDTO updateFiltersGroupDTO)
        {
            var filterGroup = await _filterGroupsRepository.GetAsync(updateFiltersGroupDTO.Id);
            filterGroup.CategoryId = updateFiltersGroupDTO.CategoryId;

            var removedItems = filterGroup.SpecificationTypes
                .ExceptBy(updateFiltersGroupDTO.SpecificationTypeIds, t => t.Id);
            foreach (var item in removedItems)
                filterGroup.SpecificationTypes.Remove(item);

            var addedItemIds = updateFiltersGroupDTO.SpecificationTypeIds
                .Except(filterGroup.SpecificationTypes.Select(t => t.Id));
            foreach (var itemId in addedItemIds)
            {
                var item = await _specificationTypesRepository.GetAsync(itemId);
                filterGroup.SpecificationTypes.Add(item);
            }

            await _filterGroupsRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the filters group by id
        /// </summary>
        /// <remarks>
        /// DELETE /filtergroups/1
        /// </remarks>
        /// <param name="id">FiltersGroup id (int)</param>
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
            await _filterGroupsRepository.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get the filters group by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /filtergroups/category/1
        /// </remarks>
        /// <param name="categoryId">Category id (int)</param>
        /// <returns>Returns FiltersGroupDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet("category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FiltersGroupDTO>> GetCategoryFiltersGroup(int categoryId) => 
            Ok(_mapper.Map<FiltersGroupDTO>(await _filterGroupsRepository.GetCategoryFiltersGroupAsync(categoryId)));
    }
}
