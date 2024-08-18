using Microsoft.EntityFrameworkCore;
using Warehouse.AuthServer.Data;
using Warehouse.AuthServer.Models.Request;
using Warehouse.AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Warehouse.AuthServer.Models.DTOs;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.InkML;

namespace Warehouse.AuthServer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IAuthService authService;

        public UserService(ApplicationDbContext dbContext,
                           IAuthService authService)
        {
            this.dbContext = dbContext;
            this.authService = authService;
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetUsersAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> CreateUserAsync(Register register)
        {
            var user = new ApplicationUser
            {
                UserName = register.UserName,
                NormalizedUserName = register.UserName,
                Email = register.Email,
                NormalizedEmail = register.Email,
                PhoneNumber = register.PhoneNumber,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PasswordHash = register.Password
            };
            var passwordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, register.Password);
            user.PasswordHash = passwordHash;
            dbContext.Users.Add(user);
            await authService.Register(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(Guid id, Register register)
        {
            var user = new ApplicationUser
            {
                UserName = register.UserName,
                NormalizedUserName = register.UserName,
                Email = register.Email,
                NormalizedEmail = register.Email,
                PhoneNumber = register.PhoneNumber,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PasswordHash = register.Password
            };
            var passwordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, register.Password);
            user.PasswordHash = passwordHash;
            dbContext.Entry(user).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<string> GenerateCSVFromUserAsync(IReadOnlyList<UsersDTO> users, string[] columns)
        {
            string filePath = Path.GetTempFileName();
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                string header = string.Empty;
                header = "Id, FirstName, LastName, Email, UserName, Password";
                await writer.WriteLineAsync(header);

                foreach (var user in users)
                {
                    var line = "";
                    line = $"{user.Id},{user.FirstName},{user.LastName},{user.Email},{user.UserName},{user.Password}";
                    await writer.WriteLineAsync(line);
                }
            }
            return filePath;
        }
        public async Task<byte[]> GenerateExcelFromUserAsync(IReadOnlyList<UsersDTO> users, byte[] columns)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "FirstName";
                worksheet.Cell(currentRow, 3).Value = "LastName";
                worksheet.Cell(currentRow, 4).Value = "Email";
                worksheet.Cell(currentRow, 5).Value = "UserName";
                worksheet.Cell(currentRow, 6).Value = "Password";
                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.Id.ToString();
                    worksheet.Cell(currentRow, 2).Value = user.FirstName;
                    worksheet.Cell(currentRow, 3).Value = user.LastName;
                    worksheet.Cell(currentRow, 4).Value = user.Email;
                    worksheet.Cell(currentRow, 5).Value = user.UserName;
                    worksheet.Cell(currentRow, 6).Value = user.Password;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }

		public async Task<IEnumerable<ApplicationUser>> GetAllDetailsAsync(CancellationToken cancellationToken)
		{
			return await dbContext.Users.ToListAsync(cancellationToken);
		}
	}
}
