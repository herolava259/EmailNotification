using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Playground.JobFinder.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionBuilder.UseSqlite("Data Source=app.db");

        return new ApplicationDbContext(optionBuilder.Options);
    }
}
