using MassTransit;
using System.Reflection;
using Warehouse.Notification.Settings;

namespace Warehouse.Notification.Configurations
{
    public static class ConfigureRabbitMQ
    {
        public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
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
            });
        }
    }
}
