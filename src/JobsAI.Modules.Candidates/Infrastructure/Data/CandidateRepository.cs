using JobsAI.Modules.Candidates.Application.Abstractions;
using JobsAI.Modules.Candidates.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace JobsAI.Modules.Candidates.Infrastructure.Data;

internal class CandidateRepository : ICandidateRepository
{
    private readonly CandidatesDbContext _dbContext;

    public CandidateRepository(CandidatesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Candidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Candidates
            .Include(c => c.CVs)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddAsync(Candidate candidate, CancellationToken cancellationToken = default)
    {
        await _dbContext.Candidates.AddAsync(candidate, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
