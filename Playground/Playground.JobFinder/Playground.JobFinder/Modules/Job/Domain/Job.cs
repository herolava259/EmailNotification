using Playground.JobFinder.Bases;
using Playground.JobFinder.Modules.Employer.EmployerDomain;
using Playground.JobFinder.Modules.JobSeeker.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.JobFinder.Modules.Job.Domain;

public enum EmploymentType: ushort
{
    FullTime = 0,
    PartTime = 1,
    Contract = 2
}

public sealed class Job: EntityBase
{
    [ComplexType]
    public sealed record SalaryRange(decimal Minimum, decimal Maximum)
    {

        public override string ToString()
        {
            return $"f{Minimum}$ - {Maximum}$";
        }
    }


    public string Title { get; set; } = "Title";

    public string Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public EmploymentType EmploymentType { get; set; }

    
    public SalaryRange? RangeSalary { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset PostedAt { get; set; }

    public DateTimeOffset ExpiresAt { get; set; }


    // Relatioships below

    public Guid RecruiterId { get; set; }
    public Recruiter? Recruiter { get; set; }

    public ICollection<Application> Applications { get; set; } = [];


    
}
