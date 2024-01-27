using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesRepository _repository;

        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesRepository repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /categories
        /// </remarks>
        /// <returns>Returns IEnumerable<CategoryDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<CategoryDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if category exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /categories/exists/1
        /// </remarks>
        /// <param name="id">Category id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        [HttpGet("exists/{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            Ok(await _repository.ExistsAsync(id));

        /// <summary>
        /// Get the category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /categories/1
        /// </remarks>
        /// <param name="id">Category id (int)</param>
        /// <returns>Returns CategoryDTO</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CategoryDTO>> Get(int id) => 
            Ok(_mapper.Map<CategoryDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a category
        /// </summary>
        /// <remarks>
        /// POST /categories
        /// {
        ///     name: "Category name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createCategoryDTO">CreateCategoryDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect category DTO was passed</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var category = await _repository.CreateAsync(_mapper.Map<Category>(createCategoryDTO));
            if (category is null) return UnprocessableEntity();
            return Ok(category.Id);
        }

        /// <summary>
        /// Full update the category
        /// </summary>
        /// <remarks>
        /// PUT /categories
        /// {
        ///     id: "1",
        ///     name: "Updated category name"
        /// }
        /// </remarks>
        /// <param name="categoryDTO">CategoryDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] CategoryDTO categoryDTO)
        {
            await _repository.UpdateAsync(_mapper.Map<Category>(categoryDTO));
            return NoContent();
        }

        /// <summary>
        /// Partially update the category
        /// </summary>
        /// <remarks>
        /// PATCH /categories
        /// {
        ///     id: "1",
        ///     name: "Updated category name"
        /// }
        /// </remarks>
        /// <param name="updateCategoryDTO">UpdateCategoryDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPatch]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _repository.GetAsync(updateCategoryDTO.Id);
            category.Name = updateCategoryDTO.Name;
            category.Description = updateCategoryDTO.Description;
            category.RootId = updateCategoryDTO.RootId;
            category.ParentId = updateCategoryDTO.ParentId;
            category.IsRootCategory = updateCategoryDTO.IsMainCategory;

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the category by id
        /// </summary>
        /// <remarks>
        /// DELETE /categories/1
        /// </remarks>
        /// <param name="id">Category id (int)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
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
        /// Get the enumeration of main categories with child categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /categories/main
        /// </remarks>
        /// <returns>Returns IEnumerable<CategoryDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet("main")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetMainCategories() =>
            Ok(_mapper.Map<IEnumerable<CategoryDTO>>(await _repository.GetMainCategoriesAsync()));
    }
}
