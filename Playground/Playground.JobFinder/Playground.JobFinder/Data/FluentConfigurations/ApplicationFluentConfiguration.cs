using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.JobFinder.Modules.JobSeekerDomain;

namespace Playground.JobFinder.Data.FluentConfigurations;

public sealed class ApplicationFluentConfiguration: EntityBaseFluentConfiguration<Application>
{
    public override void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Application");
        base.Configure(builder);

        builder.Property(c => c.Status)
               .IsRequired()
               .HasDefaultValue(ApplicationStatus.Applied)
               .HasConversion(c => c.ToString(),
                              c => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), c));

        builder.Property(c => c.AppliedAt)
               .IsRequired();

        builder.Property(c => c.ResumeSnapshot)
               .IsRequired()
               .HasDefaultValue("resume-snapshot-default")
               .HasMaxLength(4096);

        builder.Property(c => c.CoverLetter)
               .IsRequired()
               .HasDefaultValue("cover-letter-default")
               .HasMaxLength(4096);

        builder.HasOne(c => c.Job)
               .WithMany(c => c.Applications)
               .HasConstraintName("FK_Many_Application_With_One_Job")
               .HasForeignKey(c => c.JobId)
               .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(c => c.Applicant)
               .WithMany(c => c.Applications)
               .HasConstraintName("FK_Many_Application_With_One_Applicant")
               .HasForeignKey(c => c.ApplicantId)
               .OnDelete(DeleteBehavior.NoAction);

    }
}
