using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.BLL.Service
{
    public interface IAuthenticationService
    {
        public Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        public Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<bool> ConfirmEmailAsync(string token, string userId);
        public Task<ForgotPasswordRespsonse> RequestPasswordReset(ForgotPasswordRequestDTO request);
         public Task<ResetPasswordResponse> ResetPassswordAsync(ResetPasswordRequest request);

    }
}
