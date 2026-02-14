using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.JobFinder.Modules.Employer.EmployerDomain;

namespace Playground.JobFinder.Data.FluentConfigurations;

public sealed class RecruiterFluentConfiguration: EntityBaseFluentConfiguration<Recruiter>
{
    public override void Configure(EntityTypeBuilder<Recruiter> builder)
    {
        base.Configure(builder);

        builder.ToTable("Recruiter");

        builder.Property(c => c.Name).IsRequired()
                                     .HasMaxLength(128);

        builder.Property(c => c.Email).IsRequired()
                                     .HasMaxLength(64);
        builder.Property(c => c.Phone).IsRequired()
                                     .HasMaxLength(32);

        builder.Property(c => c.Company).IsRequired()
                                     .HasMaxLength(128);

        builder.HasMany(c => c.Jobs)
               .WithOne(c => c.Recruiter)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
