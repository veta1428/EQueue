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
    public class QueueConfiguration
        : IEntityTypeConfiguration<Queue>
    {
        public void Configure(EntityTypeBuilder<Queue> builder)
        {
            builder
                .HasKey(s => s.Id)
                .HasName(nameof(Queue) + nameof(Queue.Id));

            builder
                .HasOne<ScheduledClass>(q => q.ScheduledClass)
                .WithMany(sc => sc.Queues)
                .HasForeignKey(sc => sc.ScheduledClassId);

            builder.HasOne<Group>()
                .WithMany()
                .HasForeignKey(q => q.GroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
