using Microsoft.AspNetCore.Identity;
using Playground.JobFinder.Modules.EmployerDomain;
using Playground.JobFinder.Modules.JobDomain;
using Playground.JobFinder.Modules.JobSeekerDomain;

namespace Playground.JobFinder.Data;

public class ApplicationDbInitializer(IServiceScopeFactory _scopeFactory, ILogger<ApplicationDbInitializer> _logger): IDbInitializer
{
    public async Task InitializeDataAsync()
    {
        // using UserManager
        // using RoleManager
        // using ApplicationDbContext


        // add account default for test : 2 record
        // add role default for test : 2 record
        // seed 5 record for each table: job, applicant, application, recruiter

        await using var scope = _scopeFactory.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


        // TODO: use chat gpt to gen reasonable datas

        await ApplicationDbInitializer.SeedApplicationSideAsync(dbContext, _logger);

        await ApplicationDbInitializer.SeedIdentitySideAsync(userManager, roleManager, _logger);

    }

    public static async Task SeedApplicationSideAsync(ApplicationDbContext dbContext, ILogger<ApplicationDbInitializer> logger)
    {
        await Task.CompletedTask;
    }

    public static async Task SeedIdentitySideAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleMannager,
                                                    ILogger<ApplicationDbInitializer> logger)
    {
        await Task.CompletedTask;
    }

    public static IEnumerable<Job> SeedJobData()
    {
        return Enumerable.Empty<Job>();
    }


    public static IEnumerable<Recruiter> SeedRecruiterData()
    {
        return Enumerable.Empty<Recruiter>();
    }

    public static IEnumerable<Application> SeedApplicantionData()
    {
        return Enumerable.Empty<Application>();
    }

    public static IEnumerable<Applicant> SeedApplicantData()
    {
        return Enumerable.Empty<Applicant>();
    }



}
