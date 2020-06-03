using Microsoft.AspNetCore.Identity;
using System;
using VakSight.Repository.Implementation.DataAccessObjects;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    public class UserLoginRecord : IdentityUserLogin<Guid>
    {
        public virtual UserRecord User { get; set; }
    }
}
