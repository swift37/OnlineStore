using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.MenuItem;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/menu-items")]
    public class MenuItemsController : BaseController
    {
        private readonly IRepository<MenuItem> _repository;

        private readonly IMapper _mapper;

        public MenuItemsController(IRepository<MenuItem> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of menu items
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /menuitems
        /// </remarks>
        /// <returns>Returns IEnumerable<MenuItemDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<MenuItemDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if menu item exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /menuitems/exists/1
        /// </remarks>
        /// <param name="id">MenuItem id</param>
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
        /// Get the menu item by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /menuitems/1
        /// </remarks>
        /// <param name="id">MenuItem id (int)</param>
        /// <returns>Returns MenuItemDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemDTO>> Get(int id) =>
            Ok(_mapper.Map<MenuItem>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a menu item
        /// </summary>
        /// <remarks>
        /// POST /menuitems
        /// {
        ///     name: "MenuItem name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createMenuItemDTO">CreateMenuItemDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect menu item DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateMenuItemDTO createMenuItemDTO)
        {
            var menuItem = await _repository.CreateAsync(_mapper.Map<MenuItem>(createMenuItemDTO));
            if (menuItem is null) return UnprocessableEntity();
            return Ok(menuItem.Id);
        }

        /// <summary>
        /// Full update the menu item
        /// </summary>
        /// <remarks>
        /// PUT /menuitems
        /// {
        ///     id: "1",
        ///     name: "Updated menu item name"
        /// }
        /// </remarks>
        /// <param name="menuItemDTO">menuItemDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] MenuItemDTO menuItemDTO)
        {
            await _repository.UpdateAsync(_mapper.Map<MenuItem>(menuItemDTO));
            return NoContent();
        }

        /// <summary>
        /// Partially update the menu item
        /// </summary>
        /// <remarks>
        /// PATCH /menuitems
        /// {
        ///     id: "1",s
        ///     name: "Updated menu item name"
        /// }
        /// </remarks>
        /// <param name="updateMenuItemDTO">UpdateMenuItemDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateMenuItemDTO updateMenuItemDTO)
        {
            var menuItem = await _repository.GetAsync(updateMenuItemDTO.Id);
            menuItem.Name = updateMenuItemDTO.Name;
            menuItem.IsMegaMenu = updateMenuItemDTO.IsMegaMenu;
            menuItem.Image = updateMenuItemDTO.Image;
            menuItem.CategoryId = updateMenuItemDTO.CategoryId;

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the menu item by id
        /// </summary>
        /// <remarks>
        /// DELETE /menuitems/1
        /// </remarks>
        /// <param name="id">MenuItem id (int)</param>
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
