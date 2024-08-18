using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Warehouse.AuthServer.Models;

namespace Warehouse.AuthServer.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IReadOnlyList<ApplicationRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> GetRole(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> CreateRole(ApplicationRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateRole(ApplicationRole role)
        {
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRole(ApplicationRole  role)
        {
            return await _roleManager.DeleteAsync(role);
        }
    }
}
