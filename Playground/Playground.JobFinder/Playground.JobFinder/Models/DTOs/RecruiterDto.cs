namespace Playground.JobFinder.Models.DTOs
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
