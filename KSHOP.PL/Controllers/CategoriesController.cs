using KSHOP.BLL.Service;
using KSHOP.DAL.Data;
using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using KSHOP.DAL.Repository;
using KSHOP.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KSHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        readonly IStringLocalizer<SharedResources> _localizer;
        public CategoriesController(ICategoryService categoryService, IStringLocalizer<SharedResources> localizer)
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(new
            {
                data = categories,
                message = _localizer["succes"].Value
            });
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            await _categoryService.CreateCategoryAsync(request);

            return Ok(new
            {
                message = _localizer["succes"].Value
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategory(c => c.Id == id);
            if (category == null)
            {
                return NotFound(new
                {
                    message = _localizer["notfound"].Value
                });
            }
            return Ok(new
            {
                data = category,
                message = _localizer["succes"].Value
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.Delete(id);
            if (deleted)
            {
                return Ok(new
                {
                    message = _localizer["succes"].Value
                });
            }
            return NotFound(new
            {
                message = _localizer["notfound"].Value
            });

        }
        

    }
}
