using JobsAI.Modules.Candidates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsAI.Modules.Candidates.Infrastructure.Data.Configurations;

internal class JobHistoryConfiguration : IEntityTypeConfiguration<JobHistory>
{
    public void Configure(EntityTypeBuilder<JobHistory> builder)
    {
        builder.ToTable(ConfigurationConstants.JobHistoriesTable);

        builder.HasKey(jh => jh.Id);

        builder.Property(jh => jh.CandidateId)
            .IsRequired();

        builder.Property(jh => jh.CompanyName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(jh => jh.Position)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(jh => jh.StartDate)
            .IsRequired();

        builder.Property(jh => jh.EndDate);
    }
}
