using System.Text.Json;
using Warehouse.AuthServer.Services.Caches;
using Warehouse.AuthServer.Services.Users;

namespace Warehouse.AuthServer.Services.Applications
{
	public class CacheInitializer
	{
		private readonly ICacheService cacheService;
		private readonly IUserService userService;
		public CacheInitializer(ICacheService cacheService,
								IUserService usersService)
		{
			this.cacheService = cacheService;
			this.userService = usersService;
		}

		public async Task InitCacheAsync(
			ILogger logger,
			CancellationToken cancellationToken = default)
		{
			var users = await userService.GetAllDetailsAsync(cancellationToken);

			if (users.Any())
			{
				foreach (var user in users)
				{
					await cacheService.RemoveKeyAsync(user.Id.ToString());
					string jsonValue = JsonSerializer.Serialize(user);
					await cacheService.SetValueAsync(user.Id.ToString(), jsonValue);
				}
				logger.LogInformation("Migration completed: Data migrated from database to cache.");
			}
		}
	}
}
