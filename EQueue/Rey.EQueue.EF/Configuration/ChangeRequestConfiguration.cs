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
    public class ChangeRequestConfiguration : IEntityTypeConfiguration<ChangeRequest>
    {
        public void Configure(EntityTypeBuilder<ChangeRequest> builder)
        {
            builder
                .HasKey(c => c.Id)
                .HasName(nameof(ChangeRequest) + nameof(ChangeRequest.Id));

            builder
                .HasOne<Record>(r => r.RecordFrom)
                .WithMany(r => r.ChangeFrom)
                .HasForeignKey(chr => chr.RecordFromId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne<Record>(r => r.RecordTo)
                .WithMany(r => r.ChangeTo)
                .HasForeignKey(chr => chr.RecordToId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(c => c.Status)
                .HasConversion<int>();
        }
    }
}
