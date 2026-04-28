using KSHOP.BLL.Service;
using KSHOP.DAL.Data.DTO.Request;
using KSHOP.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KSHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IStringLocalizer _stringLocaizer;

        public BrandController(IBrandService brandService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _brandService = brandService;
            _stringLocaizer = stringLocalizer;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(new
            {
                data = brands,
                message = _stringLocaizer["succes"].Value
            });
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(BrandRequest request)
        {
            await _brandService.CreateBrandAsync(request);
            return Ok(new
            {
                message = _stringLocaizer["succes"].Value
            });

        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetBrand(c => c.Id == id);
            if (brand == null)
            {
                return NotFound(new
                {
                    message = _stringLocaizer["notfound"].Value
                });
            }
            return Ok(new
            {
                data = brand,
                message = _stringLocaizer["succes"].Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await _brandService.Delete(id);
            if (result)
            {
                return Ok(new
                {
                    message = _stringLocaizer["succes"].Value
                });
            }
            else
            {
                return NotFound(new
                {
                    message = _stringLocaizer["notfound"].Value
                });
            }

        }
        
    }
}
