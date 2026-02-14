using Playground.JobFinder.Bases;
using Playground.JobFinder.Modules.Job.DTOs;
using Playground.JobFinder.Modules.JobDomain;
using Playground.JobFinder.Modules.JobSeeker.Domain;

namespace Playground.JobFinder.Modules.JobSeeker.DTOs
{
    public class ApplicationDto: BaseDto
    {
        public Guid JobId { get; set; }

        public Guid ApplicantId { get; set; }

        public ApplicationStatus Status { get; set; }

        public DateTimeOffset AppliedAt { get; set; }

        public string CoverLetter { get; set; } = string.Empty;

        public string ResumeSnapshot { get; set; } = string.Empty;

        public JobDto? Job { get; set; }

        public ApplicantDto? Applicant { get; set; }
    }
}
