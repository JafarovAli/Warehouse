using StackExchange.Redis;

namespace Warehouse.AuthServer.Configurations
{
	public static class ConfigureRedis
	{
		public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IConnectionMultiplexer>(sp =>
			{
				var configure = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
				return ConnectionMultiplexer.Connect(configure);
			});

        }
	}
}
