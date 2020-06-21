using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VseTut.Core.Categories;
using VseTut.Core.Users.Model;

namespace VseTut.Web.Host.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(
            ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDto input)
        {
            try
            {
                await _categoryManager.CreateCategoryAsync(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CategoriesAsync(long? id)
        {
            var result = new List<CategoryDto>();
            try
            {
                if (id == null)
                {
                    result = await _categoryManager.GetCategories();
                    return Ok(result);
                }
                else
                {
                    var category = await _categoryManager.GetCategoryById((long)id);
                    return Ok(category);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditCategoryAsync([FromBody] CategoryDto input)
        {
            try
            {
                await _categoryManager.EditCategory(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync(long id)
        {
            try
            {
                await _categoryManager.DeleteCategory(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
