using Microsoft.AspNetCore.Identity;
using System;

namespace VakSight.Repository.Implementation.DataAccessObjects
{
    public class UserClaimRecord : IdentityUserClaim<Guid>
    {
        public virtual UserRecord User { get; set; }
    }
}
