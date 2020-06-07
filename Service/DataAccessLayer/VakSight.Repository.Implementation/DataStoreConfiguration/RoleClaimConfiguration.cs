using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VakSight.Repository.Implementation.DataAccessObjects;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    internal class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaimRecord>
    {
        public void Configure(EntityTypeBuilder<RoleClaimRecord> builder)
        {
            builder.ToTable("AspNetRoleClaims");

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
