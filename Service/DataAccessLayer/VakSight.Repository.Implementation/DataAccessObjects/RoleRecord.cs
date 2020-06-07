using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VakSight.Repository.Implementation.DataAccessObjects
{
    public class RoleRecord : IdentityRole<Guid>
    {
        public string Title { get; set; }

        public virtual ICollection<UserRoleRecord> Users { get; set; }

        public virtual ICollection<RoleClaimRecord> Claims { get; set; }
    }
}
