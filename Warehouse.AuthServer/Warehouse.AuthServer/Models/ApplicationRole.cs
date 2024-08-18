using Microsoft.AspNetCore.Identity;

namespace Warehouse.AuthServer.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
            UserRoles = new HashSet<IdentityUserRole<Guid>>();
            Users = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
