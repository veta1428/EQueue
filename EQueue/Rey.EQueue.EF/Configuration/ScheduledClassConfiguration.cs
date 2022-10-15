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
    public class ScheduledClassConfiguration
        : IEntityTypeConfiguration<ScheduledClass>
    {
        public void Configure(EntityTypeBuilder<ScheduledClass> builder)
        {
            builder
                .HasKey(s => s.Id)
                .HasName(nameof(ScheduledClass) + nameof(ScheduledClass.Id));

            builder
                .HasOne<SubjectInstance>(sc => sc.SubjectInstance)
                .WithMany(si => si.ScheduledClasses)
                .HasForeignKey(s => s.Id);
        }
    }
}
