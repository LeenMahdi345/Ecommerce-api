using KSHOP.DAL.Data;
using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IEmailSender emailSender,IConfiguration configration, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configration;
  
            _httpContextAccessor = httpContext;
        }



        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)

                return new RegisterResponse()
                {
                    Succes = false,
                    Message = "Error",
                    Error= result.Errors.Select(e => e.Description).ToList()
                };

            await _userManager.AddToRoleAsync(user, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token =Uri.EscapeDataString(token);
            var emailUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Authentication/confirm-email?token={token}&userId={user.Id}";
            await _emailSender.SendEmailAsync(
                user.Email,
                "welcome",
                $"<h1> welcome {request.UserName} </h1>" +
                $"<a href='{emailUrl}'>confirm</a>"
            );
            return new RegisterResponse()
            {
                Succes = true,
                Message = "User created successfully"
            };
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user =  await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new LoginResponse()
                {
                    Succes = false,
                    Message = "User not found"
                };
            if(!user.EmailConfirmed)
                return new LoginResponse()
                {
                    Succes = false,
                    Message = "Email not confirmed"
                };
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
                return new LoginResponse()
                {
                    Succes = false,
                    Message = "Invalid password"
                };
            return new LoginResponse()
            {
                Succes = true,
                Message = "Login successful",
                AccessToken = await GenerateAccessToken(user)
            };
        }
        private async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var userClaims = new List<Claim>()
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ConfirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);/// دأللتأكد من صحة التوكن و تأكيد الايميل يعني هل التوكن انا عملته؟ فاذا اه اكد عالايميل ويخليه ترو
            return result.Succeeded;


        }

        public async Task<ForgotPasswordRespsonse> RequestPasswordReset(ForgotPasswordRequestDTO request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ForgotPasswordRespsonse()
                {
                    Success = false,
                    Message = "Email Not Found"
                };
            }

            var random = new Random();
            var code = random.Next(1000, 9999).ToString();

            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);

            await _userManager.UpdateAsync(user);

            await _emailSender.SendEmailAsync(request.Email, "reset password", $"<p>Code Is {code}</p>");

            return new ForgotPasswordRespsonse()
            {
                Success = true,
                Message = "code sent to your email"
            };
        }

        public async Task<ResetPasswordResponse> ResetPassswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "Email Not Found"
                };
            }

            else if (user.CodeResetPassword != request.ResetCode)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "Invalid code"
                };
            }
            else if (user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "Code Expired"
                };
            }

            var isSamePassword = await _userManager.CheckPasswordAsync(user, request.NewPassword);

            if (isSamePassword)
            {

                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "New Password Must Be Different From Old Password"
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
       
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);     //بعمل تشفير وتغيير الباس

            if (!result.Succeeded)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "password reset failed"
                };
            }

            await _emailSender.SendEmailAsync(request.Email, "change password", $"<p>your password has been reset</p>");

            return new ResetPasswordResponse()
            {
                Success = true,
                Message = "password reset succcesfully"
            };
        }

    }
}
