using JobsAI.Modules.Candidates.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsAI.Modules.Candidates.Infrastructure.Data.Configurations;

internal class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);
    }
}
