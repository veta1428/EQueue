using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.Core.Entities.Security;

namespace Rey.EQueue.EF.Configuration
{
    public class UserRoleConfiguration
        : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(nameof(UserRole.UserId), nameof(UserRole.GroupId), nameof(UserRole.RoleId));

            builder
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(ur => ur.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
