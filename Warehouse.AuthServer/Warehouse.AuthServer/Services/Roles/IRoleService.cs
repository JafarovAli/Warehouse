using Microsoft.AspNetCore.Identity;
using Warehouse.AuthServer.Models;

namespace Warehouse.AuthServer.Services.Roles
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRole(ApplicationRole role);
        Task<IdentityResult> DeleteRole(ApplicationRole role);
        Task<ApplicationRole> GetRole(string id);
        Task<IReadOnlyList<ApplicationRole>> GetRoles();
        Task<IdentityResult> UpdateRole(ApplicationRole role);
    }
}