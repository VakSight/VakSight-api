using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using VakSight.Repository.Implementation.DataStoreConfiguration;
using VakSight.Shared.DataAccess;

namespace VakSight.Repository.Implementation.DataAccessObjects
{
    public class UserRecord : IdentityUser<Guid>, IBaseUserRecord
    {
        public long Key { get; set; }

        public Guid? RoleId { get; set; }
        public RoleRecord Role { get; set; }

        public virtual ICollection<UserRoleRecord> Roles { get; set; }

        public virtual ICollection<UserClaimRecord> Claims { get; set; }

        public virtual ICollection<UserLoginRecord> Logins { get; set; }

        public virtual ICollection<UserTokenRecord> Tokens { get; set; }
    }
}
