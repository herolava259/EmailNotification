using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Playground.JobFinder.Modules.EmployerDomain;
using Playground.JobFinder.Modules.JobDomain;
using Playground.JobFinder.Modules.JobSeekerDomain;

namespace Playground.JobFinder.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationAccount : IdentityUser
    {
        
    }

}
