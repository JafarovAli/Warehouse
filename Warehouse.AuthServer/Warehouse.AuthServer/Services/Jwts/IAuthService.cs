using Warehouse.AuthServer.Models;

namespace Warehouse.AuthServer.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(Login user);
        Task<bool> Login(Login user);
        Task<bool> Register(ApplicationUser user);
    }
}