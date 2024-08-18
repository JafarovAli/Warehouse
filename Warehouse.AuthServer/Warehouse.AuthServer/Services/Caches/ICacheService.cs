namespace Warehouse.AuthServer.Services.Caches
{
	public interface ICacheService
	{
		Task RemoveKeyAsync(string key);
		Task SetValueAsync(string key, string value);
	}
}