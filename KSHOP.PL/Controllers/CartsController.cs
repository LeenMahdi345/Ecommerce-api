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
using System.Security.Claims;

namespace KSHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        readonly IStringLocalizer<SharedResources> _localizer;
        public CartsController(ICartService cartService, IStringLocalizer<SharedResources> localizer)
        {
            _cartService = cartService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCart(AddToCart request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartService.AddToCartAsync(request, userId);

            if (!result)
            {
                return BadRequest(new
                {
                    message = _localizer["error"].Value
                });
            }

            return Ok(new
            {
                message = _localizer["succes"].Value
            });
        }
        [HttpGet("")]
        public async Task<IActionResult> GetCartItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartService.GetCartItems(userId);
            return Ok(new { data = cartItems });

        }
    }
}
