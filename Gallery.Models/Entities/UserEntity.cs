using Microsoft.AspNetCore.Identity;

namespace Gallery.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool UserConfirmed { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Guid? CreateUserId { get; set; }
        public Guid? LastUpdateUserId { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
