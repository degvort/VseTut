using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VseTut.Core.SubCategories;
using VseTut.Core.SubCategories.Dto;

namespace VseTut.Web.Host.Controllers
{
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryManager _subCategorymanager;

        public SubCategoryController(
            ISubCategoryManager subCategorymanager)
        {
            _subCategorymanager = subCategorymanager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSubCategoryAsync([FromBody] SubCategoryDto input)
        {
            try
            {
                await _subCategorymanager.CreateSubCategoryAsync(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditSubCategory([FromBody] SubCategoryDto input)
        {
            try
            {
                await _subCategorymanager.EditSubCategory(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteSubCategoryAsync(long id)
        {
            try
            {
                await _subCategorymanager.DeleteSubCategory(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
