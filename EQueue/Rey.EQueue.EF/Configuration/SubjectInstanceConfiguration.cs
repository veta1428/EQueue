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
    public class SubjectInstanceConfiguration
        : IEntityTypeConfiguration<SubjectInstance>
    {
        public void Configure(EntityTypeBuilder<SubjectInstance> builder)
        {
            builder
            .HasKey(s => s.Id)
            .HasName(nameof(SubjectInstance) + nameof(SubjectInstance.Id));

            builder
                .HasOne<Subject>(si => si.Subject)
                .WithMany(s => s.SubjectInstances)
                .HasForeignKey(si => si.SubjectId);
        }
    }
}
