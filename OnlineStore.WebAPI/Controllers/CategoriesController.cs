using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [Produces("application/json")]
    public class CategoriesController : BaseController
    {
        private readonly IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository) =>
            _repository = repository;

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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var result = (await _repository.GetAllAsync()).ToDTO();

            return Ok(result);
        }

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
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryDTO>> Get(int id) => 
            Ok((await _repository.GetAsync(id)).ToDTO());

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
        /// <param name="categoryDTO">CategoryDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect category DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CategoryDTO categoryDTO)
        {
            var category = await _repository.CreateAsync(categoryDTO.FromDTO());
            if (category is null) return UnprocessableEntity();
            return Ok(category.Id);
        }

        /// <summary>
        /// Update the category
        /// </summary>
        /// <remarks>
        /// PUT /categories
        /// {
        ///     name: "Updated category name"
        /// }
        /// </remarks>
        /// <param name="categoryDTO">CategoryDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] CategoryDTO categoryDTO)
        {
            await _repository.UpdateAsync(categoryDTO.FromDTO());
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
