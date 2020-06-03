using Microsoft.AspNetCore.Identity;
using System;
using VakSight.Repository.Implementation.DataAccessObjects;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    public class UserTokenRecord : IdentityUserToken<Guid>
    {
        public virtual UserRecord User { get; set; }
    }
}
