using Playground.JobFinder.Bases;

namespace Playground.JobFinder.Modules.JobSeeker.DTOs;


public class ApplicantDto: BaseDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string? ResumeUrl { get; set; }

    public ICollection<ApplicationDto> Applications { get; set; } = [];
}
