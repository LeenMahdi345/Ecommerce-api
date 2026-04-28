using KSHOP.BLL.Service;
using KSHOP.DAL.Repository;
using KSHOP.DAL.Utils;

namespace KSHOP.PL.Extention
{
    public static class ApplicationServicesExtention
    {
            public static IServiceCollection AddApplicationServices(this IServiceCollection Services, IConfiguration Configuration)
            {

            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddTransient<IEmailSender, EmailSender>();
            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IBrandRepository, BrandRepository>();
            Services.AddScoped<IBrandService, BrandService>();
            Services.AddScoped<ICartRepository, CartRepository>();
            Services.AddScoped<ICartService, CartService>();
            return Services;
        }
    }
}
