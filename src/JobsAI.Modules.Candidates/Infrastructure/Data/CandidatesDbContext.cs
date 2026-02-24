using JobsAI.Modules.Candidates.Domain.Aggregates;
using JobsAI.Modules.Candidates.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsAI.Modules.Candidates.Infrastructure.Data;

internal class CandidatesDbContext : DbContext
{
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<CV> CVs => Set<CV>();
    public DbSet<JobHistory> JobHistories => Set<JobHistory>();

    public CandidatesDbContext(DbContextOptions<CandidatesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Configurations.ConfigurationConstants.Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CandidatesDbContext).Assembly);
    }
}
