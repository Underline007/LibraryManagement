using LibraryManagement.Application.Dtos.Category;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    namespace LibraryManagement.API.Controllers
    {
        [ApiController]
        [Route("api/categories")]
        public class CategoryController : ControllerBase
        {
            private readonly ICategoryService _categoryService;
            private readonly ILogger<CategoryController> _logger;

            public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
            {
                _categoryService = categoryService;
                _logger = logger;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllCategories(int pageNumber = 1, int pageSize = 10)
            {
                try
                {
                    var categories = await _categoryService.GetAllCategoryAsync(pageNumber, pageSize);
                    return Ok(categories);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while retrieving categories.");
                    return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving categories.");
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetCategoryById(Guid id)
            {
                try
                {
                    var category = await _categoryService.GetCategoryByIdAsync(id);
                    if (category == null)
                    {
                        return NotFound();
                    }
                    return Ok(category);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while retrieving the category.");
                    return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the category.");
                }
            }

            [Authorize(Roles = nameof(UserRole.SuperUser))]
            [HttpPost]
            public async Task<IActionResult> AddCategory([FromBody] CategoryCreateEditDto createEditCategoryDto)
            {
                try
                {
                    if (createEditCategoryDto == null)
                    {
                        return BadRequest();
                    }

                    var category = await _categoryService.AddCategoryAsync(createEditCategoryDto);
                    return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding the category.");
                    return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while adding the category.");
                }
            }

            [Authorize(Roles = nameof(UserRole.SuperUser))]
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCategory(Guid id)
            {

                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    throw new KeyNotFoundException("category not found");
                }
                try
                {
                    
                    await _categoryService.DeleteCategoryAsync(id);
                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogWarning(ex, "Category not found with id: {Id}", id);
                    return NotFound();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the category.");
                    return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the category.");
                }
            }

            [Authorize(Roles = nameof(UserRole.SuperUser))]
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryCreateEditDto createEditCategoryDto)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(id, createEditCategoryDto);
                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogWarning(ex, "Category not found with id: {Id}", id);
                    return NotFound();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the category.");
                    return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating the category.");
                }
            }
        }
    }

}
