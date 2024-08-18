using Warehouse.Notification.Services.Caches;
using Warehouse.Notification.Services.Email;
using Warehouse.Notification.Settings;

namespace Warehouse.Notification.Configurations
{
    public static class ConfigureServices
    {
        public static void AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICacheService, CacheService>();

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<ConnectionStringsRedis>(configuration.GetSection("ConnectionStrings"));
        }
    }
}
