using System;
using System.Collections.Generic;
using System.Text;

namespace VakSight.Shared
{
    public static class AuthConsts
    {
        public static class Users
        {
            public static Guid SuperAdministratorId = new Guid("17B13F39-0CB8-457B-A40F-8EDB5B811535");
        }

        public static class Roles
        {
            public static Guid SuperAdministratorId = new Guid("4B71370E-E052-4F5A-9F6B-56D9AE79D885");
            public const string SuperAdministrator = "SuperAdministrator";
        }
    }
}
