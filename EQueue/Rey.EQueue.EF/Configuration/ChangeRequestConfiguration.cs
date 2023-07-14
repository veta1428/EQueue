using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities;

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
                .HasOne<User>(r => r.UserFrom)
                .WithMany()
                .HasForeignKey(chr => chr.UserFromId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .HasOne<User>(r => r.UserTo)
                .WithMany()
                .HasForeignKey(chr => chr.UserToId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .HasOne<Queue>()
                .WithMany()
                .HasForeignKey(chr => chr.QueueId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(c => c.Status)
                .HasConversion<int>();
        }
    }
}
