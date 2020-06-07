using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Shared;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleRecord>
    {
        public void Configure(EntityTypeBuilder<UserRoleRecord> builder)
        {
            builder.ToTable("AspNetUserRoles");

            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(x => x.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            builder.HasData(new UserRoleRecord { UserId = AuthConsts.Users.SuperAdministratorId, RoleId = AuthConsts.Roles.SuperAdministratorId });
        }
    }
}
