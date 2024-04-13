using Microsoft.AspNetCore.Identity;

namespace Gallery.Models.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string RolePersianName { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
