using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Models.Request;

namespace Warehouse.AuthServer.Services.Jwts
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }
        public async Task<bool> Register(ApplicationUser user)
        {
           
            var identityUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.UserName,
            };
            var result = await userManager.CreateAsync(identityUser, user.PasswordHash);
            return result.Succeeded;
        }

        public async Task<bool> Login(Login user)
        {
            var identityUser = await userManager.FindByNameAsync(user.UserName);
            if (identityUser is null)
            {
                return false;
            }
            return await userManager.CheckPasswordAsync(identityUser, user.Password);

        }

        public string GenerateTokenString(Login user)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.Role,"Admin"),
                //new Claim(ClaimTypes.NameIdentifier,id.ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value));

            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securtyToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: config.GetSection("Jwt:Issuer").Value,
                audience: config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securtyToken);
            return tokenString;
        }
    }
}
