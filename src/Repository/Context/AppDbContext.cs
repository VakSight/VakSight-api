using Microsoft.EntityFrameworkCore;
using Repository.DatabaseModels;
using Repository.DataStoreConfigurations;

namespace Repository.Context
{
    public class AppDbContext : DbContext
    {
        internal DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO set relationships between tables
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);            
        }
    }
}