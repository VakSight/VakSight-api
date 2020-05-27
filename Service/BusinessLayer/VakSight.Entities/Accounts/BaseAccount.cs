using Newtonsoft.Json;
using System;

namespace VakSight.Entities.Accounts
{
    public class BaseAccount
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
