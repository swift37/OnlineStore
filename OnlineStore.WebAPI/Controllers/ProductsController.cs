using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <response code="404">Not Found</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            await _repository.ExistsAsync(id) ? Ok(true) : NotFound(false);

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
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _repository.GetAsync(id);
            if (product is null) return NotFound();

            return Ok(product.ToDTO());
        }

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
        /// <param name="productDTO">ProductDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect product DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] ProductDTO productDTO)
        {
            var product = await _repository.CreateAsync(productDTO.FromDTO());
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
        /// <param name="productDTO">ProductDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] ProductDTO productDTO)
        {
            await _repository.UpdateAsync(productDTO.FromDTO());
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
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            await _repository.DeleteAsync(id) ? NoContent() : NotFound();

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductsPageDTO>> GetProductByCategory(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15, 
            IProductsRepository.SortParameters sortBy = IProductsRepository.SortParameters.Default)
        {
            var prodcutsPage = await _repository
                .GetProductsByCategoryAsync(categoryId, page, itemsPerPage, sortBy);

            if (prodcutsPage == null) return NotFound();

            return Ok(prodcutsPage.ToDTO());
        }

    }
}
