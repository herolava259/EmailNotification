using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Playground.JobFinder.Bases;
using Playground.JobFinder.Data.FluentConfigurations;
using Playground.JobFinder.Modules.Employer.EmployerDomain;
using Playground.JobFinder.Modules.Job.Domain;
using Playground.JobFinder.Modules.JobSeeker.Domain;

namespace Playground.JobFinder.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationAccount>(options)
    {


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #region dbSet
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Recruiter> Recruiers { get; set; }

        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<Application> Applications { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            try
            {
                builder.ApplyConfiguration(new JobFluentConfiguration());
                builder.ApplyConfiguration(new RecruiterFluentConfiguration());
                builder.ApplyConfiguration(new ApplicantFluentConfiguration());
                builder.ApplyConfiguration(new ApplicationFluentConfiguration());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
