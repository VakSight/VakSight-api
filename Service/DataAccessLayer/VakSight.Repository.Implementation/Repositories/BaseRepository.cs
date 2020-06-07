using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using VakSight.Repository.Contracts;
using VakSight.Repository.Implementation.Auth;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Shared;
using VakSight.Shared.Exceptions;

namespace VakSight.Repository.Implementation.Repositories
{
    public abstract class BaseRepository : IBaseRepository<Context, SignInManager>
    {
        protected Context Context { get; private set; }
        protected SignInManager SignInManager { get; private set; }

        protected const string AccessTokenProvider = JwtTokenProvider.Name;
        protected const string AccessTokenPurpose = "access";
        protected const string RefreshTokenProvider = "Default";
        protected const string RefreshTokenPurpose = "refresh";
        protected const string PasswordTokenProvider = "Default";
        protected const string PasswordTokenPurpose = "password";

        public void SetContext(Context context)
        {
            Context = context;
        }

        public void SetSignInManager(SignInManager signInManager)
        {
            SignInManager = signInManager;
        }

        protected virtual string EntityName { get => "Item"; }

        protected void ValidateIsFound(object key, object item, string entityName = null)
        {
            if (item == null)
            {
                throw new ItemNotFoundException(string.Format(ErrorMessageConsts.NotFound, entityName ?? EntityName, key));
            }
        }

        protected void ValidateCollection<T>(IEnumerable<T> data)
        {
            if (data == null || data.Count() == 0)
            {
                throw new ItemNotFoundException("Could not find item(s)");
            }
        }

        protected void ValidateIdentityResult(IdentityResult identityResult, UserRecord user = null)
        {
            if (!identityResult.Succeeded)
            {
                var conflictFieldErrors = new Dictionary<string, string>();

                if (identityResult.Errors.Any(x => x.Code == IdentityConsts.Codes.DuplicateEmail))
                {
                    conflictFieldErrors.Add(nameof(user.Email), ErrorMessageConsts.DuplicateEmail);
                }

                var invalidTokenErrors = identityResult.Errors.Where(x => x.Code == IdentityConsts.Codes.InvalidToken);
                if (invalidTokenErrors.Any())
                {
                    conflictFieldErrors.Add("Token", string.Join(Environment.NewLine, invalidTokenErrors.Select(x => x.Description)));
                }

                if (conflictFieldErrors.Any())
                {
                    throw new ConflictException(ErrorMessageConsts.InvalidInputFields, conflictFieldErrors);
                }

                var errorMessage = string.Join(Environment.NewLine, identityResult.Errors.Select(x => x.Description));
                throw new ApplicationException(errorMessage);
            }
        }
    }
}
