using Playground.JobFinder.Modules.JobDomain;

namespace Playground.JobFinder.Modules.EmployerDomain;

public sealed class Recruiter : EntityBase
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Company { get; set; }

    public ICollection<Job> Jobs { get; set; } = [];
}
