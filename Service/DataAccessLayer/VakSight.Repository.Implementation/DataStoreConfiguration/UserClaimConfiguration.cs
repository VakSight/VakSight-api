using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VakSight.Repository.Implementation.DataAccessObjects;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    internal class UserClaimConfiguration : IEntityTypeConfiguration<UserClaimRecord>
    {
        public void Configure(EntityTypeBuilder<UserClaimRecord> builder)
        {
            builder.ToTable("AspNetUserClaims");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.UserId);
        }
    }
}
