using MassTransit;
using System.Reflection;
using Warehouse.AuthServer.Jobs;
using Warehouse.AuthServer.Models.DTOs;
using Warehouse.AuthServer.Settings;
using Warehouse.CommonLibrary.DTOs;

namespace Warehouse.AuthServer.Configurations;

public static class ConfigureMassTransit
{
    public static void AddMassTransit(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMassTransit(massTransit =>
        {
            var rabbitMQSettings = configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();

            massTransit.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));
            massTransit.AddConsumers(Assembly.GetExecutingAssembly());
            massTransit.UsingRabbitMq((context, config) =>
            {
                config.Host(new Uri(rabbitMQSettings.RabbitMqRootUri), rb =>
                {
                    rb.Username(rabbitMQSettings.UserName);
                    rb.Password(rabbitMQSettings.Password);
                });
                config.ConfigureEndpoints(context);
            });
            services.AddScoped<IMessageQueue<UsersDownloadAllDataDTO>, MessageQueue<UsersDownloadAllDataDTO>>();
            services.AddScoped<IMessageQueue<UserDownloadUrlDTO>, MessageQueue<UserDownloadUrlDTO>>();
        });
    }
}