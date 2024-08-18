using Microsoft.EntityFrameworkCore;
using Warehouse.AuthServer.Data;

namespace Warehouse.AuthServer.Configurations
{
    public static class ConfigureDb
    {
        public static void AddConfigureDbServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetSection("ConnectionStrings:Warehouse").Value)
            );
        }
    }
}
