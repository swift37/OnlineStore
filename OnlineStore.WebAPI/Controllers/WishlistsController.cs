using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Wishlist;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class WishlistsController : BaseController
    {
        private readonly IWishlistsRepository _repository;

        private readonly IMapper _mapper;

        public WishlistsController(IWishlistsRepository repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of wishlists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /wishlists
        /// </remarks>
        /// <returns>Returns IEnumerable<WishlistDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<WishlistDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<WishlistDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if wishlist exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /wishlists/exists/1
        /// </remarks>
        /// <param name="id">Wishlist id</param>
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
        /// Get the wishlist by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /wishlists/1
        /// </remarks>
        /// <param name="id">Wishlist id (int)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<WishlistDTO>> Get(int id) => 
            Ok(_mapper.Map<WishlistDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a wishlist
        /// </summary>
        /// <remarks>
        /// POST /wishlists
        /// {
        ///     name: "Wishlist name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createWishlistDTO">CreateWishlistDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect wishlist DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> Create([FromBody] CreateWishlistDTO createWishlistDTO)
        {
            var wishlist = _mapper.Map<Wishlist>(createWishlistDTO);
            wishlist.UserId = UserId;
            wishlist.CreationDate = DateTime.UtcNow;
            wishlist.LastChangeDate = DateTime.UtcNow;

            var createdWishlist = await _repository.CreateAsync(wishlist);
            if (createdWishlist is null) return UnprocessableEntity();
            return Ok(createdWishlist.Id);
        }

        /// <summary>
        /// Full update the wishlist
        /// </summary>
        /// <remarks>
        /// PUT /wishlists
        /// {
        ///     id: "1",
        ///     name: "Updated wishlist name"
        /// }
        /// </remarks>
        /// <param name="wishlistDTO">WishlistDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] WishlistDTO wishlistDTO)
        {
            await _repository.UpdateAsync(_mapper.Map<Wishlist>(wishlistDTO));
            return NoContent();
        }

        /// <summary>
        /// Partially update the wishlist
        /// </summary>
        /// <remarks>
        /// PATCH /wishlists
        /// {
        ///     id: "1",
        ///     name: "Updated wishlist name"
        /// }
        /// </remarks>
        /// <param name="updateWishlistDTO">UpdateWishlistDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPatch]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateWishlistDTO updateWishlistDTO)
        {
            var wishlist = await _repository.GetAsync(updateWishlistDTO.Id);
            wishlist.LastChangeDate = DateTime.Now;

            var removedItems = wishlist.Items
                .ExceptBy(updateWishlistDTO.Items.Select(t => t.Id), t => t.Id);
            foreach (var item in removedItems)
                wishlist.Items.Remove(item);

            var addedItems = updateWishlistDTO.Items
                .ExceptBy(wishlist.Items.Select(t => t.Id), t => t.Id);
            foreach (var item in addedItems)
                wishlist.Items.Add(_mapper.Map<WishlistItem>(item));

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the wishlist by id
        /// </summary>
        /// <remarks>
        /// DELETE /wishlists/1
        /// </remarks>
        /// <param name="id">Wishlist id (int)</param>
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
        /// Get the wishlist by user id
        /// </summary>
        /// <remarks>
        /// GET /wishlists/user/33CCBEBD-FB8A-4919-B727-B2782502E69E
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("user/{userId:Guid}")]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<WishlistDTO>> GetUserWishlist(Guid userId) => 
            Ok(_mapper.Map<WishlistDTO>(await _repository.GetUserWishlistAsync(userId)));

        /// <summary>
        /// Get the current user wishlist
        /// </summary>
        /// <remarks>
        /// GET /wishlists/user/current
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/current")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WishlistDTO>> GetUserWishlist()
        {
            var temp = _mapper.Map<WishlistDTO>(await _repository.GetOrCreateAsync(UserId));
            return Ok(temp);
        }
            

        /// <summary>
        /// Add an item to the wishlist
        /// </summary>
        /// <remarks>
        /// PATCH /wishlists/items
        /// {
        ///     productId: 1,
        ///     quantity: 1
        /// }
        /// </remarks>
        /// <param name="model">CreateWishlistItemDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("items")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddItem(CreateWishlistItemDTO model)
        {
            await _repository.AddItem(UserId, _mapper.Map<WishlistItem>(model));
            return NoContent();
        }

        /// <summary>
        /// Update the item of the wishlist
        /// </summary>
        /// <remarks>
        /// PATCH /wishlists/items
        /// {
        ///     id: 1,
        ///     productId: 1,
        ///     quantity: 1
        /// }
        /// </remarks>
        /// <param name="model">UpdateWishlistItemDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut("items")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateItem(UpdateWishlistItemDTO model)
        {
            await _repository.UpdateItem(UserId, _mapper.Map<WishlistItem>(model));
            return NoContent();
        }

        /// <summary>
        /// Remove the item by id from the wishlist
        /// </summary>
        /// <remarks>
        /// PATCH /wishlists/items
        /// {
        ///     id: 1
        /// }
        /// </remarks>
        /// <param name="itemId">WishlistItem Id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("items")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            await _repository.RemoveItem(UserId, itemId);
            return NoContent();
        }

        /// <summary>
        /// Remove the items by id from the wishlist
        /// </summary>
        /// <remarks>
        /// PATCH /wishlists/items
        /// {
        ///     itemIds: [1]
        /// }
        /// </remarks>
        /// <param name="itemIds">WishlistItem Ids</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("items/many")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveItems(ICollection<int> itemIds)
        {
            await _repository.RemoveItems(UserId, itemIds);
            return NoContent();
        }
    }
}
