using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Warehouse.AuthServer.Data;
using Warehouse.AuthServer.Models;

namespace Warehouse.AuthServer.Configurations
{
    public static class ConfigureJWT
    {
        public static void AddConfigureJWT(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));

                options.AddPolicy("SuperUser", policy => policy.RequireClaim(ClaimTypes.Role, "admin")
                                                             .RequireClaim(ClaimTypes.Role, "guest"));
            });
        }
    }
}
