using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    internal class UserTokenConfiguration : IEntityTypeConfiguration<UserTokenRecord>
    {
        public void Configure(EntityTypeBuilder<UserTokenRecord> builder)
        {
            builder.ToTable("AspNetUserTokens");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}
