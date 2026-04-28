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
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;
        readonly IStringLocalizer<SharedResources> _localizer;
        public ProductsController(IProductService productsService, IStringLocalizer<SharedResources> localizer)
        {
            _productsService = productsService;
            _localizer = localizer;
        }


        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]ProductRequest request)
        {
            await _productsService.CreateProductAsync(request);

            return Ok(new
            {
                message = _localizer["succes"].Value
            });
        }
        [HttpGet("all")]
      
        public async Task<IActionResult> GetAll()
        {
            var products = await _productsService.GetAllProductsAsync();
            return Ok(new
            {
                data = products,
                message = _localizer["succes"].Value
            });
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> Index(int id)
        {
            var products = await _productsService.GetProduct(p=>p.Id==id);
            if(products==null)
                return NotFound();
            return Ok(new
            {
                data = products,
                message = _localizer["succes"].Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productsService.Delete(id);
            return Ok(new
            {
                message = _localizer["succes"].Value
            });
        }

           [HttpPatch("{id}")]
           public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateRequest request)
        {
                        var updated = await _productsService.UpdateProductAsync(id, request);
            if (!updated)
            {
                return NotFound(new
                {
                    message = _localizer["notfound"].Value
                });
            }
            return Ok(new
            {
                message = _localizer["succes"].Value
            });
        }
        [HttpPatch("status/{id}")]
        public async Task <IActionResult> ChangeStatus (int id)
        {
            var updated = await _productsService.ToggleStatus(id);
            if (!updated)
            {
                return NotFound(new
                {
                    message = _localizer["notfound"].Value
                });
            }
            return Ok(new
            {
                message = _localizer["succes"].Value
            });
        }


        

    }
}
