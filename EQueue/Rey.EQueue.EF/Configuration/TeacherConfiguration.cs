using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Configuration
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder
                .HasKey(t => t.Id)
                .HasName(nameof(Teacher) + nameof(Teacher.Id));

            builder.HasOne<Group>()
                .WithMany()
                .HasForeignKey(t => t.GroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
