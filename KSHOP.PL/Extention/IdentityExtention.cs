using KSHOP.DAL.Data;
using KSHOP.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KSHOP.PL.Extention
{
    public static class IdentityExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;           // 0-9
                options.Password.RequireLowercase = true;       // a-z
                options.Password.RequireUppercase = true;       // A-Z
                options.Password.RequireNonAlphanumeric = true; // ! @ # $ %
                options.Password.RequiredLength = 10;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return Services;
        }

    }
}
