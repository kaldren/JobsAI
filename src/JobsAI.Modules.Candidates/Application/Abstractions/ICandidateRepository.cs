using JobsAI.Modules.Candidates.Application.Dtos;

namespace JobsAI.Modules.Candidates.Application.Abstractions;

public interface ICandidateRepository
{
    public Task UpdateCV(CVDto cv, CancellationToken cancellationToken = default);
}
