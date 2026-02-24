using JobsAI.Modules.Candidates.Domain.Entities;
using JobsAI.Modules.Candidates.Domain.ValueObjects;

namespace JobsAI.Modules.Candidates.Domain.Aggregates;

internal class Candidate
{
    private readonly List<CV> _cvs = [];

    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyCollection<CV> CVs => _cvs.AsReadOnly();

    private Candidate() { }

    public Candidate(Guid id, Name name, Email email, Phone phone, DateOnly dateOfBirth)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        DateOfBirth = dateOfBirth;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddCV(CV cv)
    {
        _cvs.Add(cv);
    }
}
