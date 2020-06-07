using Microsoft.AspNetCore.Identity;
using System;

namespace VakSight.Repository.Implementation.DataAccessObjects
{
    public class RoleClaimRecord : IdentityRoleClaim<Guid>
    {
        public virtual RoleRecord Role { get; set; }
    }
}
