using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.ProductTag;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/product-tags")]
    public class ProductTagsController : BaseController
    {
        private readonly IRepository<ProductTag> _repository;

        private readonly IMapper _mapper;

        public ProductTagsController(IRepository<ProductTag> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of product tags
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /product-tags
        /// </remarks>
        /// <returns>Returns IEnumerable<ProductTagDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ProductTagDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<ProductTagDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if product tag exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /product-tags/exists/1
        /// </remarks>
        /// <param name="id">ProductTag id</param>
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
        /// Get the product tag by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /product-tags/1
        /// </remarks>
        /// <param name="id">ProductTag id (int)</param>
        /// <returns>Returns ProductTagDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProductTagDTO>> Get(int id) =>
            Ok(_mapper.Map<ProductTagDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a product tag
        /// </summary>
        /// <remarks>
        /// POST /product-tags
        /// {
        ///     name: "ProductTag name",
        ///     colorHex: "#fff"
        /// }
        /// </remarks>
        /// <param name="createProductTagDTO">CreateProductTagDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="422">If the incorrect productTag DTO was passed</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductTagDTO createProductTagDTO)
        {
            var productTag = _mapper.Map<ProductTag>(createProductTagDTO);

            if (await _repository.CreateAsync(productTag) is null)
                return UnprocessableEntity();

            return Ok(productTag.Id);
        }

        /// <summary>
        /// Partially update the product tag
        /// </summary>
        /// <remarks>
        /// PATCH /product-tags
        /// {
        ///     id: "1",
        ///     name: "Updated product tag name"
        /// }
        /// </remarks>
        /// <param name="updateProductTagDTO">UpdateProductTagDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateProductTagDTO updateProductTagDTO)
        {
            var productTag = await _repository.GetAsync(updateProductTagDTO.Id);
            productTag.Name = productTag.Name;
            productTag.ColorHex = productTag.ColorHex;

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the product tag by id
        /// </summary>
        /// <remarks>
        /// DELETE /product-tags/1
        /// </remarks>
        /// <param name="id">ProductTag id (int)</param>
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
