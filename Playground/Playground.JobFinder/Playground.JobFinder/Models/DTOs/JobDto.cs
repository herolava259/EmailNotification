using Playground.JobFinder.Modules.EmployerDomain;
using Playground.JobFinder.Modules.JobDomain;
using static Playground.JobFinder.Modules.JobDomain.Job;

namespace Playground.JobFinder.Models.DTOs
{
    public class JobDto
    {
        public Guid RecruiterId { get; set; }

        public string Title { get; set; } = "Title";

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public EmploymentType EmploymentType { get; set; }


        public SalaryRange? RangeSalary { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset PostedAt { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        public RecruiterDto? Recruiter { get; set; }

        public ICollection<ApplicationDto> Applications { get; set; } = [];
    }
}
