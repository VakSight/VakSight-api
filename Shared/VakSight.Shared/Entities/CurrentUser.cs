using System;
using System.Net;

namespace VakSight.Shared.Entities
{
    public class CurrentUser
    {
        public Guid Id { get; set; }

        public IPAddress IPAddress { get; set; }

        public string TokenPurpose { get; set; }

        public string Roles { get; set; }
    }
}
