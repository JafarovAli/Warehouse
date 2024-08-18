using StackExchange.Redis;
using System.Threading.Tasks;

namespace Warehouse.Notification.Services.Caches
{
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer redis;

        public CacheService(IConnectionMultiplexer redis)
        {
            this.redis = redis;
        } 

        public async Task<string> GetUserFullNameAsync(string userId)
        {
            var db = redis.GetDatabase();
            return await db.StringGetAsync($"user:{userId}:fullname");
        }

        public async Task<string> GetEmailAsync(string userId)
        {
            var db = redis.GetDatabase();
            return await db.StringGetAsync($"user:{userId}:email");
        }
    }
}
