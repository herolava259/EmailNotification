using Playground.JobFinder.Bases;
using Playground.JobFinder.Modules.Employer.DTOs;
using Playground.JobFinder.Modules.EmployerDomain;
using Playground.JobFinder.Modules.Job.Domain;
using Playground.JobFinder.Modules.JobSeeker.DTOs;
using static Playground.JobFinder.Modules.Job.Domain.Job;

namespace Playground.JobFinder.Modules.Job.DTOs;

public class JobDto: BaseDto
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

public class JobDtoView: JobDto
{
    public string DecriptionView
    {
        get
        {
            if (Description.Length <= 64)
                return Description;

            return Description.Substring(0, 61) + "...";
        }
    }

    public string RangeSalaryView
    {
        get
        {
            if (RangeSalary == null)
                return "Attractive Salary";

            return RangeSalary.ToString();
        }
    }
}
