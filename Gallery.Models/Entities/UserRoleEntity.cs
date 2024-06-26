﻿using Microsoft.AspNetCore.Identity;

namespace Gallery.Models.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
