using Playground.JobFinder.Bases;
using Playground.JobFinder.Modules.Job.DTOs;

namespace Playground.JobFinder.Modules.Employer.DTOs
{
    public class RecruiterDto: BaseDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }

        public ICollection<JobDto> Jobs { get; set; } = [];
    }
}
