using Playground.JobFinder.Modules.JobDomain;
using Playground.JobFinder.Modules.JobSeekerDomain;

namespace Playground.JobFinder.Models.DTOs
{
    public class ApplicationDto: BaseDto
    {
        public Guid JobId { get; set; }

        public Guid ApplicantId { get; set; }

        public ApplicationStatus Status { get; set; }

        public DateTimeOffset AppliedAt { get; set; }

        public string CoverLetter { get; set; } = String.Empty;

        public string ResumeSnapshot { get; set; } = String.Empty;

        public JobDto? Job { get; set; }

        public ApplicantDto? Applicant { get; set; }
    }
}
