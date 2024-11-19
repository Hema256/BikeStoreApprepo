using Microsoft.AspNetCore.Mvc;

namespace BikeStore_BackEnd.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
   /* using BikeStore_BackEnd.DTOs;
    using BikeStore_BackEnd.Services;*/
    using global::BikeStore_BackEnd.Dto;
    using global::BikeStore_BackEnd.Iservices;

    namespace BikeStore_BackEnd.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CategoryController : ControllerBase
        {
            private readonly ICategoryService _categoryService;

            public CategoryController(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

            // GET: api/category
            [HttpGet]
            public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }

            // GET: api/category/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }

            // POST: api/category
            [HttpPost]
            public async Task<ActionResult<CategoryDTO>> AddCategory(CategoryDTO categoryDto)
            {
                var createdCategory = await _categoryService.AddCategoryAsync(categoryDto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
            }

            // PUT: api/category/{id}
            [HttpPut("{id}")]
            public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, CategoryDTO categoryDto)
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
                if (updatedCategory == null)
                {
                    return NotFound();
                }
                return Ok(updatedCategory);
            }

            // DELETE: api/category/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCategory(int id)
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent(); // 204 No Content
            }
        }
    }

}