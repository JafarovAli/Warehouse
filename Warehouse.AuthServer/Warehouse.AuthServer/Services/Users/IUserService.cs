using Minio;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Models.DTOs;
using Warehouse.AuthServer.Models.Request;

namespace Warehouse.AuthServer.Services.Users
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUserAsync(Register register);
        Task DeleteUserAsync(Guid id);
        Task<ApplicationUser> GetUserByIdAsync(Guid id);
        Task<IReadOnlyList<ApplicationUser>> GetUsersAsync();
        Task UpdateUserAsync(Guid id, Register register);
        Task<string> GenerateCSVFromUserAsync(IReadOnlyList<UsersDTO> users, string[] columns);
        Task<byte[]> GenerateExcelFromUserAsync(IReadOnlyList<UsersDTO> users, byte[] columns);
		Task<IEnumerable<ApplicationUser>> GetAllDetailsAsync(CancellationToken cancellationToken);
	}
}