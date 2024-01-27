using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.NestedMenuItem;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.Persistence.Repositories;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/nested-menu-items")]
    public class NestedMenuItemsController : BaseController
    {
        private readonly IRepository<NestedMenuItem> _nestedMenuItemsRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        private readonly IMapper _mapper;

        public NestedMenuItemsController(
            IRepository<NestedMenuItem> nestedMenuItemsRepository,
            ICategoriesRepository categoriesRepository,
        IMapper mapper) =>
            (_nestedMenuItemsRepository, _categoriesRepository, _mapper) = 
            (nestedMenuItemsRepository, categoriesRepository, mapper);

        /// <summary>
        /// Get the enumeration of nested menu items
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /nestedmenuitems
        /// </remarks>
        /// <returns>Returns IEnumerable<NestedMenuItemDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NestedMenuItemDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<NestedMenuItemDTO>>(await _nestedMenuItemsRepository.GetAllAsync()));

        /// <summary>
        /// Get true if nested menu item exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /nestedmenuitems/exists/1
        /// </remarks>
        /// <param name="id">NestedMenuItem id</param>
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
            Ok(await _nestedMenuItemsRepository.ExistsAsync(id));

        /// <summary>
        /// Get the nested menu item by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /nestedmenuitems/1
        /// </remarks>
        /// <param name="id">NestedMenuItem id (int)</param>
        /// <returns>Returns NestedMenuItemDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NestedMenuItemDTO>> Get(int id) =>
            Ok(_mapper.Map<NestedMenuItem>(await _nestedMenuItemsRepository.GetAsync(id)));

        /// <summary>
        /// Create a nested menu item
        /// </summary>
        /// <remarks>
        /// POST /nestedmenuitems
        /// {
        ///     name: "NestedMenuItem name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createNestedMenuItemDTO">CreateNestedMenuItemDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect nested menu item DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateNestedMenuItemDTO createNestedMenuItemDTO)
        {
            var nestedMenuItem = _mapper.Map<NestedMenuItem>(createNestedMenuItemDTO);

            foreach (var categoryId in createNestedMenuItemDTO.CategoryIds)
            {
                var category = await _categoriesRepository.GetAsync(categoryId);
                nestedMenuItem.Categories.Add(category);
            };

            if (await _nestedMenuItemsRepository.CreateAsync(nestedMenuItem) is null) 
                return UnprocessableEntity();

            return Ok(nestedMenuItem.Id);
        }

        /// <summary>
        /// Full update the nested menu item
        /// </summary>
        /// <remarks>
        /// PUT /nestedmenuitems
        /// {
        ///     id: "1",
        ///     name: "Updated nested menu item name"
        /// }
        /// </remarks>
        /// <param name="nestedMenuItemDTO">nestedMenuItemDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] NestedMenuItemDTO nestedMenuItemDTO)
        {
            await _nestedMenuItemsRepository.UpdateAsync(_mapper.Map<NestedMenuItem>(nestedMenuItemDTO));
            return NoContent();
        }

        /// <summary>
        /// Partially update the nested menu item
        /// </summary>
        /// <remarks>
        /// PATCH /nestedmenuitems
        /// {
        ///     id: "1",s
        ///     name: "Updated nested menu item name"
        /// }
        /// </remarks>
        /// <param name="updateNestedMenuItemDTO">UpdateNestedMenuItemDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateNestedMenuItemDTO updateNestedMenuItemDTO)
        {
            var nestedMenuItem = await _nestedMenuItemsRepository.GetAsync(updateNestedMenuItemDTO.Id);
            nestedMenuItem.Name = updateNestedMenuItemDTO.Name;
            nestedMenuItem.ParentId = updateNestedMenuItemDTO.ParentId;
            nestedMenuItem.HasTwoColumns = updateNestedMenuItemDTO.HasTwoColumns;

            var removedItems = nestedMenuItem.Categories
                 .ExceptBy(updateNestedMenuItemDTO.CategoryIds, t => t.Id);
            foreach (var item in removedItems)
                nestedMenuItem.Categories.Remove(item);

            var addedItemIds = updateNestedMenuItemDTO.CategoryIds
                .Except(nestedMenuItem.Categories.Select(t => t.Id));
            foreach (var itemId in addedItemIds)
            {
                var category = await _categoriesRepository.GetAsync(itemId);
                nestedMenuItem.Categories.Add(category);
            };

            await _nestedMenuItemsRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the nested menu item by id
        /// </summary>
        /// <remarks>
        /// DELETE /nestedmenuitems/1
        /// </remarks>
        /// <param name="id">NestedMenuItem id (int)</param>
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
            await _nestedMenuItemsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
