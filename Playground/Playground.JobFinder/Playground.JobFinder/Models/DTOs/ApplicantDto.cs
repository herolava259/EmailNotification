namespace Playground.JobFinder.Models.DTOs;


public class ApplicantDto: BaseDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; } = String.Empty;

    public string? ResumeUrl { get; set; }

    public ICollection<ApplicationDto> Applications { get; set; } = [];
}
