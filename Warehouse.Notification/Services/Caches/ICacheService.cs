namespace Warehouse.Notification.Services.Caches
{
    public interface ICacheService
    {
        Task<string> GetUserFullNameAsync(string userId);
        Task<string> GetEmailAsync(string userId);
    }
}
