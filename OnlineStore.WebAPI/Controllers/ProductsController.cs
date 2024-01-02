using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Enums;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class ProductsController : BaseController
    {
        private readonly IProductsRepository _productsRepository;

        private readonly IReviewsRepository _reviewsRepository;

        public ProductsController(IProductsRepository productsRepository, IReviewsRepository reviewsRepository) => 
            (_productsRepository, _reviewsRepository) = (productsRepository, reviewsRepository);

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
            Ok((await _productsRepository.GetAllAsync()).ToDTO());

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) => 
            Ok(await _productsRepository.ExistsAsync(id));

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
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var productDTO = (await _productsRepository.GetAsync(id)).ToDTO();
            productDTO.Rating = await _reviewsRepository.GetProductRatingAsync(id);
            productDTO.ReviewsCount = await _reviewsRepository.GetReviewsCountByProductAsync(id);
            return Ok(productDTO);
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
            var product = await _productsRepository.CreateAsync(createProductDTO.FromDTO());
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
            await _productsRepository.UpdateAsync(updateProductDTO.FromDTO());
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
            await _productsRepository.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get the products page by category id
        /// </summary>
        /// <remarks>
        /// POST /products/page
        /// {
        ///     categoryId: 1,
        ///     pageNumber: 1,
        ///     itemsPerPage: 15
        /// }
        /// </remarks>
        /// <param name="options">Optins for product filtering</param>
        /// <returns>Returns ProductsPageDTO</returns>
        /// <response code="200">Success</response>
        [HttpPost("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductsPageDTO>> GetFilteredProducts(
            ProductsFilteringOptions options)
        {
            var pageDTO = (await _productsRepository.GetFilteredProductsAsync(options)).ToDTO();

            pageDTO.Products = SortProducts(pageDTO.Products, options.SortBy);

            foreach (var product in pageDTO.Products)
            {
                product.Rating = await _reviewsRepository.GetProductRatingAsync(product.Id);
                product.ReviewsCount = await _reviewsRepository.GetReviewsCountByProductAsync(product.Id);
            }

            return Ok(pageDTO);
        }

        private ICollection<ProductDTO> SortProducts(ICollection<ProductDTO> products, SortParameter sortBy)
        {
            switch (sortBy)
            {
                default:
                    return products;
                case SortParameter.RatingDescending:
                    return products.OrderByDescending(p => p.Rating).ToArray();
                case SortParameter.PriceAscending:
                    return products.OrderBy(p => p.UnitPrice).ToArray();
                case SortParameter.PriceDescending:
                    return products.OrderByDescending(p => p.UnitPrice).ToArray();
            }
        }
    }
}
