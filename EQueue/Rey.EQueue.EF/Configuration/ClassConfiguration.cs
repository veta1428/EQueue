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
    public class ClassConfiguration
        : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder
                .HasKey(c => c.Id)
                .HasName(nameof(Class) + nameof(Class.Id));

            builder
                .HasOne<Timetable>(c => c.Timetable)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TimetableId);
        }
    }
}
