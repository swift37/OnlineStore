using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Enums;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [Produces("application/json")]
    public class ProductsController : BaseController
    {
        private readonly IProductsRepository _repository;

        public ProductsController(IProductsRepository repository) => 
            _repository = repository;

        /// <summary>
        /// Get the enumeration of products
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products
        /// </remarks>
        /// <returns>Returns IEnumerable<ProductDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll() => 
            Ok((await _repository.GetAllAsync()).ToDTO());

        /// <summary>
        /// Get true if product exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products/exists/1
        /// </remarks>
        /// <param name="id">Product id</param>
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
        /// Get the product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products/1
        /// </remarks>
        /// <param name="id">Product id (int)</param>
        /// <returns>Returns ProductDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDTO>> Get(int id) => 
            Ok((await _repository.GetAsync(id)).ToDTO());

        /// <summary>
        /// Create a product
        /// </summary>
        /// <remarks>
        /// POST /products
        /// {
        ///     name: "Product name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createProductDTO">CreateProductDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect product DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductDTO createProductDTO)
        {
            var product = await _repository.CreateAsync(createProductDTO.FromDTO());
            if (product is null) return UnprocessableEntity();
            return Ok(product.Id);
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <remarks>
        /// PUT /products
        /// {
        ///     name: "Updated product name"
        /// }
        /// </remarks>
        /// <param name="updateProductDTO">UpdateProductDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO updateProductDTO)
        {
            await _repository.UpdateAsync(updateProductDTO.FromDTO());
            return NoContent();
        }

        /// <summary>
        /// Delete the product by id
        /// </summary>
        /// <remarks>
        /// DELETE /products/1
        /// </remarks>
        /// <param name="id">Product id (int)</param>
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
        /// Get the products page by category id
        /// </summary>
        /// <remarks>
        /// GET /products/page?categoryId=1&page=3&itemsPerPage=20
        /// </remarks>
        /// <param name="categoryId">Category id (int)</param>
        /// <param name="page">Page number</param>
        /// <param name="itemsPerPage">Number of items per page</param>
        /// <param name="sortBy">Sort by statement</param>
        /// <returns>Returns ProductsPageDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductsPageDTO>> GetProductsByCategory(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15, 
            SortParameters sortBy = SortParameters.Default) => 
            Ok((await _repository.GetProductsByCategoryAsync(categoryId, page, itemsPerPage, sortBy))
                .ToDTO());

    }
}
