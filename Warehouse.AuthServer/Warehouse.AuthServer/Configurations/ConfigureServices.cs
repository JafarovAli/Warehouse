using Warehouse.AuthServer.Services.Users;
using Warehouse.AuthServer.Services.Jwts;
using Warehouse.AuthServer.Services.Roles;
using Warehouse.AuthServer.Services;
using FluentValidation.AspNetCore;
using MassTransit.Transports.Fabric;
using Warehouse.AuthServer.Services.MiniIOs;
using Microsoft.Extensions.Configuration;
using Warehouse.AuthServer.Settings;
using Warehouse.AuthServer.Services.Caches;
using Warehouse.AuthServer.Services.Applications;

namespace Warehouse.AuthServer.Configurations
{
    public static class ConfigureServices
    {
        public static void AddConfigureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMiniIOService, MiniIOService>();
            services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));
			services.AddMemoryCache();
			services.AddScoped<ICacheService, CacheService>();
            services.AddTransient<CacheInitializer>();
		}
    }
}
