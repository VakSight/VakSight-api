using Microsoft.AspNetCore.Identity;
using System;

namespace VakSight.Repository.Implementation.DataAccessObjects
{
    public class UserRoleRecord : IdentityUserRole<Guid>
    {
        public virtual RoleRecord Role { get; set; }

        public virtual UserRecord User { get; set; }
    }
}
