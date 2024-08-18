using Microsoft.AspNetCore.Identity;

namespace Warehouse.AuthServer.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            UserRoles = new HashSet<IdentityUserRole<Guid>>();
            Roles = new HashSet<ApplicationRole>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }
        public bool IsActive()
        {
            if (!LockoutEnabled)
            {
                return true;
            }

            if (LockoutEnd == null)
            {
                return true;
            }

            return DateTime.UtcNow > LockoutEnd.Value;
        }
    }
}
