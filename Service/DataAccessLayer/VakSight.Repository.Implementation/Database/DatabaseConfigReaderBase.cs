using System;
using System.Collections.Generic;

namespace VakSight.Repository.Implementation.Database
{
    public abstract class DatabaseConfigReaderBase : IDatabaseConfigReader
    {
        private Dictionary<string, DatabaseConfig> databases;

        public DatabaseConfig Get(string database)
        {
            if (databases == null)
            {
                databases = ReadConfiguration();
            }

            if (!databases.ContainsKey(database))
            {
                throw new ApplicationException($"Connection string for database '{database}' is not configured.");
            }

            return databases[database];
        }

        protected abstract Dictionary<string, DatabaseConfig> ReadConfiguration();
    }
}
