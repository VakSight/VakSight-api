using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace VakSight.Repository.Implementation.Database
{
    public class DatabaseConfigReader : DatabaseConfigReaderBase
    {
        private readonly IConfiguration configuration;

        public DatabaseConfigReader(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override Dictionary<string, DatabaseConfig> ReadConfiguration()
        {
            return configuration.GetSection("Databases").Get<Dictionary<string, DatabaseConfig>>();
        }
    }
}
