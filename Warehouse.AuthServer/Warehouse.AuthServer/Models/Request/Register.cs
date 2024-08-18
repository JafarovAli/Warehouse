using System.ComponentModel.DataAnnotations;

namespace Warehouse.AuthServer.Models.Request
{
    public class Register
    {
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Password { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
