using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities;

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
                .HasOne<User>(u => u.User)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.UserId);

            builder
                .HasOne<Record>()
                .WithMany()
                .HasForeignKey(r => r.NextRecordId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
