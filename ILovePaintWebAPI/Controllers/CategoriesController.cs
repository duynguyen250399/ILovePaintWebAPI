using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.CategoryService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAllCategories();
            if(categories.ToList().Count == 0)
            {
                return NotFound("Categories not found!");
            }

            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound($"Categoriy with id {id} not found!");
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            if(category == null)
            {
                return BadRequest("Category not found!");
            }

            return Ok(await _categoryService.AddCategory(category));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            Category category = await _categoryService.DeleteCategory(id);
            if(category == null)
            {
                return NotFound($"Category with id {category.ID} not found!");
            }

            return Ok(category);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(Category newCategory)
        {
            if(newCategory == null)
            {
                return BadRequest("Invalid category!");
            }

            if(newCategory.ID == 0)
            {
                return BadRequest("Missing Category ID field!");
            }

            Category oldCategory = _categoryService.GetCategoryById(newCategory.ID);
            if(oldCategory == null)
            {
                return NotFound($"Category with id {newCategory.ID} not found!");
            }

            Category category = await _categoryService.UpdateCategory(newCategory);

            return Ok(category);
        }
    }
}