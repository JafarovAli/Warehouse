using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.AuthServer.Enums;
using Warehouse.AuthServer.Jobs;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Models.DTOs;
using Warehouse.AuthServer.Models.Request;
using Warehouse.AuthServer.Services;
using Warehouse.AuthServer.Services.Caches;
using Warehouse.AuthServer.Services.Users;

namespace Warehouse.AuthServer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly IMessageQueue<UsersDownloadAllDataDTO> messageAllQueue;
        public UsersController(IUserService userService,
                               IAuthService authService,
                               IMessageQueue<UsersDownloadAllDataDTO> messageAllQueue )
        {
            this.userService = userService;
            this.authService = authService;
            this.messageAllQueue = messageAllQueue;
        }

        [HttpGet()]
        public async Task<IReadOnlyList<ApplicationUser>> GetUsers()
        {
            return await userService.GetUsersAsync();
        }

        [HttpGet("{id}")]
        //[Authorize("SuperUser")]
        public async Task<ActionResult<ApplicationUser>> GetUsers(Guid id)
        {
            return await userService.GetUserByIdAsync(id);
        }
        [HttpPost()]
       // [Authorize("AdminOnly")]
        public async Task<ActionResult<ApplicationUser>> CreateUsers(Register register)
        {
            return await userService.CreateUserAsync(register);
        }


        [HttpPut()]
        //[Authorize("AdminOnly")]
        public async Task<IActionResult> UpdateUsers(Guid id, Register register)
        {
            await userService.UpdateUserAsync(id, register);

            return NoContent();
        }

        [HttpDelete()]
       // [Authorize("AdminOnly")]
        public async Task<IActionResult> DeleteUsers(Guid id)
        {
            await userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("{userId}/{downloadType}/download-all-data")]
        public async Task<IActionResult> DownloadUsersAllData(DownloadType downloadType)
        {

            var userId = HttpContext.GetUserIdFromToken();
            await messageAllQueue.Enqueue(new UsersDownloadAllDataDTO(userId, downloadType));
            return Ok();
        }
    }
}
