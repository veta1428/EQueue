using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.EF.Configuration
{
    public class SubjectInstanceTeacherConfiguration
        : IEntityTypeConfiguration<SubjectInstanceTeacher>
    {
        public void Configure(EntityTypeBuilder<SubjectInstanceTeacher> builder)
        {
            builder
                .HasKey(s => s.Id)
                .HasName(nameof(SubjectInstanceTeacher) + nameof(SubjectInstanceTeacher.Id));

            builder
                .HasOne<Teacher>(t => t.Teacher)
                .WithMany(s => s.SubjectInstanceTeachers)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<SubjectInstance>(t => t.SubjectInstance)
                .WithMany(s => s.SubjectInstanceTeachers)
                .HasForeignKey(s => s.SubjectInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
