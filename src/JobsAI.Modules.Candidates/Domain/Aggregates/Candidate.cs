using JobsAI.Modules.Candidates.Domain.ValueObjects;

namespace JobsAI.Modules.Candidates.Domain.Aggregates;

internal class Candidate
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }

    public Candidate(Guid id, Name name)
    {
        Id = id;
        Name = name;
    }
}
