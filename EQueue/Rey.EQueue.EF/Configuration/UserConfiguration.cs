using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.EF.Configuration
{
    internal class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(t => t.Id)
                .HasName(nameof(User) + nameof(User.Id));

            builder
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<User>(x => x.ApplicationUserId);
        }
    }
}
