using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.JobFinder.Modules.EmployerDomain;
using Playground.JobFinder.Modules.JobDomain;

namespace Playground.JobFinder.Data.FluentConfigurations;

public sealed class JobFluentConfiguration : EntityBaseFluentConfiguration<Job>
{
    public override void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Job");
        base.Configure(builder);

        builder.Property(c => c.IsActive).HasDefaultValue(true);

        builder.Property(c => c.PostedAt).IsRequired();

        builder.ComplexProperty(c => c.RangeSalary);

        builder.Property(c => c.Title).IsRequired()
                                      .HasDefaultValue("Job-Title")
                                      .HasMaxLength(512);

        builder.Property(c => c.Description).IsRequired()
                                      .HasDefaultValue("Job-Description")
                                      .HasMaxLength(10_000);



        builder.Property(c => c.Location).IsRequired()
                                      .HasDefaultValue("Job-Location")
                                      .HasMaxLength(1024);

        builder.Property(c => c.EmploymentType).IsRequired()
                                        .HasDefaultValue(EmploymentType.Contract)
                                        .HasConversion(v => v.ToString(),
                                                        v => (EmploymentType)Enum.Parse(typeof(EmploymentType), v));

        builder.Property(c => c.IsActive).IsRequired()
                                         .HasDefaultValue(true);

        builder.Property(c => c.ExpiresAt).IsRequired();


        builder.HasOne(c => c.Recruiter)
               .WithMany(c => c.Jobs)
               .HasForeignKey(c => c.RecruiterId)
               .HasConstraintName("FK_Many_Jobs_With_One_Recruiter")
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.Applications)
               .WithOne(c => c.Job)
               .HasConstraintName("FK_One_Job_With_Many_Applicants")
               .HasForeignKey(c => c.JobId)
               .OnDelete(DeleteBehavior.NoAction);

    }
}
