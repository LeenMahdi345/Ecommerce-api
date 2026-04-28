
using KSHOP.BLL.Mapping;
using KSHOP.BLL.Service;
using KSHOP.DAL.Data;
using KSHOP.DAL.Models;
using KSHOP.DAL.Repository;
using KSHOP.DAL.Utils;
using KSHOP.PL.Extention;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KSHOP.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDataBase(builder.Configuration);
                builder.Services.AddIdentityServices();
            builder.Services.AddAuthenticationServices(builder.Configuration);
                builder.Services.AddLocalizationServices();
            builder.Services.AddCorsPolicyServices();
            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
            MapesterConfig.MapesterConfigRegister();
      
            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


            app.UseHttpsRedirection();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seed in seeders)
                {
                    await seed.DataSeed();
                }
            }

            app.Run();
        }
    }
}
