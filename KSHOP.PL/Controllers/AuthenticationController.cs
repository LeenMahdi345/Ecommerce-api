using KSHOP.BLL.Service;
using KSHOP.DAL.Data.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> confirmEmail(string token, string userId)
        {
            var isconfirmd = await _authenticationService.ConfirmEmailAsync(token, userId);
            return Ok(new
            {
                message = "OKKK"
            });
        }
        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset(ForgotPasswordRequestDTO request)
        {
            var result = await _authenticationService.RequestPasswordReset(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _authenticationService.ResetPassswordAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }




    }




}
