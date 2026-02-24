namespace JobsAI.Modules.Candidates.Domain.Entities;

internal class CV
{
    public Guid Id { get; private set; }
    public Guid CandidateId { get; private set; }
    public string Title { get; private set; }
    public string Summary { get; private set; }

    private CV() { }

    public CV(Guid id, Guid candidateId, string title, string summary)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        ArgumentException.ThrowIfNullOrWhiteSpace(summary, nameof(summary));

        Id = id;
        CandidateId = candidateId;
        Title = title;
        Summary = summary;
    }
}
