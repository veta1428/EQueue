using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Configuration
{
    public class TimetableConfiguration
        : IEntityTypeConfiguration<Timetable>
    {
        public void Configure(EntityTypeBuilder<Timetable> builder)
        {
            builder
                .HasKey(t => t.Id)
                .HasName(nameof(Timetable) + nameof(Timetable.Id));

            builder
                .HasOne<SubjectInstance>()
                .WithMany(si => si.Timetables);
        }
    }
}
