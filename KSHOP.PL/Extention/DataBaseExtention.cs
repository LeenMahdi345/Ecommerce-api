using KSHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace KSHOP.PL.Extention
{
    public static class DataBaseExtention
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
