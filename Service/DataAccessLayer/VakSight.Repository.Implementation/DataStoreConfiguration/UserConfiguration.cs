using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Shared;

namespace VakSight.Repository.Implementation.DataStoreConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserRecord>
    {
        public void Configure(EntityTypeBuilder<UserRecord> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Key)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Property(p => p.Email)
                .IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.Roles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Claims)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Logins)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Tokens)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            builder.HasData(
                new UserRecord()
                {
                    Id = AuthConsts.Users.SuperAdministratorId,
                    Email = "test@gmail.com",
                    NormalizedEmail = "TEST@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "IRK5FK4GIYQPEVVYW6DTEUA2N6IIXJUC",
                    ConcurrencyStamp = "bfc5d27d-d08f-4957-b70c-5aa1bde4b420",
                    RoleId = AuthConsts.Roles.SuperAdministratorId
                });
        }
    }
}
