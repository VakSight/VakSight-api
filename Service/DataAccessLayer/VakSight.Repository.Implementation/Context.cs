using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Repository.Implementation.Database;
using VakSight.Repository.Implementation.DataStoreConfiguration;

namespace VakSight.Repository.Implementation
{
    public class Context : IdentityDbContext<UserRecord, RoleRecord, Guid, UserClaimRecord, UserRoleRecord, UserLoginRecord, RoleClaimRecord, UserTokenRecord>, IBaseContext<UserRecord>
    {
        private readonly string DatabaseName = "VSDatabase";

        private readonly string connectionString;
        private readonly int commandTimeout;

        public Context(IDatabaseConfigProvider databaseConfigProvider)
        {
            connectionString = databaseConfigProvider.GetConnectionString(DatabaseName);
            commandTimeout = databaseConfigProvider.Get(DatabaseName).CommandTimeout;
        }

        public new virtual DbSet<UserRecord> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(connectionString,
            sqlServerOptions => sqlServerOptions.CommandTimeout(commandTimeout));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
        }

        public class DesignTimeActivitiesDbContextFactory : IDesignTimeDbContextFactory<Context>
        {
            public Context CreateDbContext(string[] args)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                    .AddJsonFile("appsettings.json", optional: false);

                var config = builder.Build();
                var configReader = new DatabaseConfigReader(config);

                return new Context(new DatabaseConfigProvider(configReader));
            }
        }
    }
}
