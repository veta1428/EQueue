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
    public class RecordConfiguration
        : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder
                .HasKey(s => s.Id)
                .HasName(nameof(Record) + nameof(Record.Id));

            builder
                .HasOne<Queue>(r => r.Queue)
                .WithMany(q => q.Records)
                .HasForeignKey(r => r.QueueId);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.QueueId);
        }
    }
}
