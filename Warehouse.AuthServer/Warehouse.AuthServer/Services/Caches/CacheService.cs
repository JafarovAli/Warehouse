using Microsoft.Extensions.Caching.Memory;

namespace Warehouse.AuthServer.Services.Caches
{
	public class CacheService : ICacheService
	{
		private readonly IMemoryCache memoryCache;

		public CacheService(IMemoryCache memoryCache)
		{
			this.memoryCache = memoryCache;
		}

		public async Task SetValueAsync(string key, string value)
		{
			await Task.Run(() => memoryCache.Set(key, value));
		}

		public async Task RemoveKeyAsync(string key)
		{
			await Task.Run(() => memoryCache.Remove(key));
		}
	}
}
