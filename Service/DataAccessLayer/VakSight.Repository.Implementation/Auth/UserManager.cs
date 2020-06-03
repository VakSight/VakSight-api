using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Repository.Implementation.DataStoreConfiguration;

namespace VakSight.Repository.Implementation.Auth
{
    public class UserManager : UserManager<UserRecord>
    {
        public UserManager(IUserStore<UserRecord> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserRecord> passwordHasher, IEnumerable<IUserValidator<UserRecord>> userValidators,
            IEnumerable<IPasswordValidator<UserRecord>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserRecord>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
            optionsAccessor.Value.User.RequireUniqueEmail = true;
            optionsAccessor.Value.SignIn.RequireConfirmedEmail = false;
        }

        public static UserManager Create<TContext>(TContext context, IServiceProvider serviceProvider) where TContext : DbContext
        {
            var optionsAccessor = serviceProvider.Resolve<IOptions<IdentityOptions>>();
            var passwordHasher = serviceProvider.Resolve<IPasswordHasher<UserRecord>>();
            var userValidators = serviceProvider.Resolve<IEnumerable<IUserValidator<UserRecord>>>();
            var passwordValidators = serviceProvider.Resolve<IEnumerable<IPasswordValidator<UserRecord>>>();
            var keyNormalizer = serviceProvider.Resolve<ILookupNormalizer>();
            var errors = serviceProvider.Resolve<IdentityErrorDescriber>();
            var logger = serviceProvider.Resolve<ILogger<UserManager>>();
            var userStore = new UserStore<UserRecord, IdentityRole<Guid>, TContext, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, UserLoginRecord, UserTokenRecord, IdentityRoleClaim<Guid>>(context, errors)
            {
                AutoSaveChanges = true,
            };

            return new UserManager(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, serviceProvider, logger);
        }
    }
}
