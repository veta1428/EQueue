using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities.Security;

namespace Rey.EQueue.EF.Configuration
{
    public class RoleConfiguration
        : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(r => r.Id)
                .HasName(nameof(Role) + nameof(Role.Id));

            builder
                .HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}
