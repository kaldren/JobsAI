using JobsAI.Modules.Candidates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsAI.Modules.Candidates.Infrastructure.Data.Configurations;

internal class CVConfiguration : IEntityTypeConfiguration<CV>
{
    public void Configure(EntityTypeBuilder<CV> builder)
    {
        builder.ToTable(ConfigurationConstants.CVsTable);

        builder.HasKey(cv => cv.Id);

        builder.Property(cv => cv.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(cv => cv.Summary)
            .HasMaxLength(4000)
            .IsRequired();
    }
}
