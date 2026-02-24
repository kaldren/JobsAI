using JobsAI.Modules.Candidates.Domain.Aggregates;

namespace JobsAI.Modules.Candidates.Application.Abstractions;

internal interface ICandidateRepository
{
    Task<Candidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Candidate candidate, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
