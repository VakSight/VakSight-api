using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    internal class UserLoginConfiguration : IEntityTypeConfiguration<UserLoginRecord>
    {
        public void Configure(EntityTypeBuilder<UserLoginRecord> builder)
        {
            builder.ToTable("AspNetUserLogins");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Logins)
                .HasForeignKey(x => x.UserId);
        }
    }
}
