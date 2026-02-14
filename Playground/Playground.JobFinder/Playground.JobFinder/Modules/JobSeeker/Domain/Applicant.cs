using Playground.JobFinder.Bases;

namespace Playground.JobFinder.Modules.JobSeeker.Domain
{
    public sealed class Applicant: EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? ResumeUrl { get; set; }


        public ICollection<Application> Applications { get; set; } = [];
    }
}
