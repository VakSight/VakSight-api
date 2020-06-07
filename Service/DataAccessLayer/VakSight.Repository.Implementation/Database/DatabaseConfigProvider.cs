using System;
using System.Text;

namespace VakSight.Repository.Implementation.Database
{
    public class DatabaseConfigProvider : IDatabaseConfigProvider
    {
        private readonly IDatabaseConfigReader configReader;

        public DatabaseConfigProvider(IDatabaseConfigReader configReader)
        {
            this.configReader = configReader;
        }

        public DatabaseConfig Get(string database)
        {
            return this.configReader.Get(database);
        }

        public string GetConnectionString(string database)
        {
            var dbConfig = Get(database);

            if (string.IsNullOrWhiteSpace(dbConfig.Server))
            {
                throw new ApplicationException($"Server for database '{database}' is not configured.");
            }

            if (string.IsNullOrWhiteSpace(dbConfig.Name))
            {
                throw new ApplicationException($"Name for database '{database}' is not configured.");
            }

            var connectionString = new StringBuilder();

            var serverName = dbConfig.IsRemote ? $"tcp:{dbConfig.Server}" : dbConfig.Server;
            connectionString.Append($"Server={serverName};");
            connectionString.Append($"Database={dbConfig.Name};");

            if (!string.IsNullOrWhiteSpace(dbConfig.Username))
            {
                connectionString.Append($"User Id={dbConfig.Username};");
            }

            if (!string.IsNullOrWhiteSpace(dbConfig.Password))
            {
                connectionString.Append($"Password={dbConfig.Password};");
            }

            connectionString.Append("Trusted_Connection=False;");

            if (dbConfig.IsRemote)
            {
                connectionString.Append("Encrypt=true;");
            }

            return connectionString.ToString();
        }
    }
}
