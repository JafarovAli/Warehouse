using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Services;
using Warehouse.AuthServer.Services.Jwts;
using Warehouse.AuthServer.Services.Roles;
using System;
using Warehouse.AuthServer.Models.Request;

namespace Warehouse.AuthServer.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize("AdminOnly")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService _roleService)
        {
            this.roleService = _roleService;
        }

        [HttpGet]
        public async Task<IReadOnlyList<IdentityRole<Guid>>> GetRoles()
        {
            return await roleService.GetRoles();
        }

        [HttpGet("{id}")]
        public async Task<IdentityRole<Guid>> GetRole(string id)
        {
            return await roleService.GetRole(id);
        }

        [HttpPost]
        public async Task<IdentityResult> CreateRoles(RegisterRole register)
        {
            var role = new ApplicationRole
            {
                Name = register.Role,
                NormalizedName = register.Role
            };
            return await roleService.CreateRole(role);
        }

        [HttpDelete()]
        public async Task<IdentityResult> DeleteRole(ApplicationRole role)
        {
            return await roleService.DeleteRole(role);
        }


    }
}