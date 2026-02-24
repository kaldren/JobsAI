namespace JobsAI.Modules.Candidates.Domain.Entities;

internal class JobHistory
{
    public Guid Id { get; private set; }
    public Guid CandidateId { get; private set; }
    public string CompanyName { get; private set; }
    public string Position { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    private JobHistory() { }
    public JobHistory(Guid id, Guid candidateId, string companyName, string position, DateOnly startDate, DateOnly? endDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(companyName, nameof(companyName));
        ArgumentException.ThrowIfNullOrWhiteSpace(position, nameof(position));
        Id = id;
        CandidateId = candidateId;
        CompanyName = companyName;
        Position = position;
        StartDate = startDate;
        EndDate = endDate;
    }
}
