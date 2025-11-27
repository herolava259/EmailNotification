using Microsoft.EntityFrameworkCore.Design;

namespace Playground.JobFinder.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        throw new NotImplementedException();
    }
}
