using Playground.JobFinder.Bases;
using Playground.JobFinder.Modules.JobDomain;

namespace Playground.JobFinder.Modules.JobSeeker.Domain;

public enum ApplicationStatus: ushort
{
    Applied,
    Screening,
    Interview,
    Offer,
    Rejected,
}

public sealed class Application : EntityBase
{

    public ApplicationStatus Status { get; set; }

    public DateTimeOffset AppliedAt { get; set; }

    public string CoverLetter { get; set; } = string.Empty;

    public string ResumeSnapshot { get; set; } = string.Empty;


    public Guid JobId { get; set; }
    public Job? Job { get; set; }

    public Guid ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }
}
