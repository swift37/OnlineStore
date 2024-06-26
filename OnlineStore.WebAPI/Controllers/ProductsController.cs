﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
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

        private readonly IMapper _mapper;

        public ProductsController(
            IProductsRepository productsRepository, 
            IReviewsRepository reviewsRepository,
            IMapper mapper) => 
            (_productsRepository, _reviewsRepository, _mapper) = 
            (productsRepository, reviewsRepository, mapper);

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
            Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productsRepository.GetAllAsync()));

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
            var productDTO = _mapper.Map<ProductDTO>(await _productsRepository.GetAsync(id));
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
            var product = await _productsRepository.CreateAsync(_mapper.Map<Product>(createProductDTO));
            if (product is null) return UnprocessableEntity();
            return Ok(product.Id);
        }

        /// <summary>
        /// Partially update the product
        /// </summary>
        /// <remarks>
        /// PATCH /products
        /// {
        ///     id: "1",
        ///     name: "Updated product name"
        /// }
        /// </remarks>
        /// <param name="updateProductDTO">UpdateProductDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO updateProductDTO)
        {
            var product = await _productsRepository.GetAsync(updateProductDTO.Id);
            product.Name = updateProductDTO.Name;
            product.Description = updateProductDTO.Description;
            product.Image = updateProductDTO.Image;
            product.CategoryId = updateProductDTO.CategoryId;
            product.UnitCost = updateProductDTO.UnitCost;
            product.UnitPrice = updateProductDTO.UnitPrice;
            product.UnitsInStock = updateProductDTO.UnitsInStock;
            product.Discount = updateProductDTO.Discount;
            product.Manufacturer = updateProductDTO.Manufacturer;
            product.ManufacturersCode = updateProductDTO.ManufacturersCode;
            product.StoreCode = updateProductDTO.StoreCode;
            product.Availability = updateProductDTO.Availability;
            product.Status = updateProductDTO.Status;

            var removedItems = product.Specifications
                .ExceptBy(updateProductDTO.Specifications.Select(t => t.Id), t => t.Id);
            foreach (var item in removedItems)
                product.Specifications.Remove(item);

            var addedItems = updateProductDTO.Specifications
                .ExceptBy(product.Specifications.Select(t => t.Id), t => t.Id);
            foreach (var item in removedItems)
                product.Specifications.Add(item);

            await _productsRepository.SaveChangesAsync();

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
        /// <param name="options">Options for product filtering</param>
        /// <returns>Returns ProductsPageDTO</returns>
        /// <response code="200">Success</response>
        [HttpPost("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductsPageDTO>> GetFilteredProducts(
            ProductsFilteringOptionsDTO options)
        {
            var optionsModel = _mapper.Map<ProductsFilteringOptions>(options);
            var pageDTO = _mapper.Map<ProductsPageDTO>(await _productsRepository.GetFilteredProductsAsync(optionsModel));

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

        /// <summary>
        /// Get products by tag id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products/tag/1
        /// </remarks>
        /// <param name="id">Product tag id (int)</param>
        /// <returns>Returns IEnumerable<ProductDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet("tag/{tagId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllByTag(int tagId) =>
            Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productsRepository.GetAllByTagAsync(tagId)));

        /// <summary>
        /// Get products by tag name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products/tag/name/sale
        /// </remarks>
        /// <param name="id">Product tag name (string)</param>
        /// <returns>Returns IEnumerable<ProductDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet("tag/name/{tagName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllByTag(string tagName) =>
            Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productsRepository.GetAllByTagAsync(tagName)));

        /// <summary>
        /// Get products by status
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products/status/1
        /// </remarks>
        /// <param name="id">Product status (ProductStatus)</param>
        /// <returns>Returns IEnumerable<ProductDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet("status/{productStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllByStatus(ProductStatus productStatus) =>
            Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productsRepository.GetAllByStatusAsync(productStatus)));

        /// <summary>
        /// Get products by availability
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /products/availability/1
        /// </remarks>
        /// <param name="id">Product availability (ProductAvailability)</param>
        /// <returns>Returns IEnumerable<ProductDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet("availability/{productAvailability}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllByAvailability(ProductAvailability productAvailability) =>
            Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productsRepository.GetAllByAvailabilityAsync(productAvailability)));
    }
}
