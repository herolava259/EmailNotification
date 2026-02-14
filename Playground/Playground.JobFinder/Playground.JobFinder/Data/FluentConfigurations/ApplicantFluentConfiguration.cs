using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.JobFinder.Modules.EmployerDomain;
using Playground.JobFinder.Modules.JobSeeker.Domain;

namespace Playground.JobFinder.Data.FluentConfigurations;

public sealed class ApplicantFluentConfiguration: EntityBaseFluentConfiguration<Applicant>
{
    public override void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("Applicant");
        base.Configure(builder);

        builder.Property(c => c.FirstName)
               .IsRequired()
               .HasDefaultValue("first-name-default")
               .HasMaxLength(64);

        builder.Property(c => c.LastName)
               .IsRequired()
               .HasDefaultValue("last-name-default")
               .HasMaxLength(64);

        builder.Property(c => c.Email)
               .IsRequired()
               .HasDefaultValue("emaildefault@email.com")
               .HasMaxLength(128);

        builder.Property(c => c.PhoneNumber)
               .IsRequired()
               .HasDefaultValue("01234567890")
               .HasMaxLength(11)
               ;
        builder.Property(c => c.ResumeUrl)
               .IsRequired()
               .HasDefaultValue("https://jobfinder/resume/resumedefault.pdf")
               .HasMaxLength(128);

        builder.HasMany(c => c.Applications)
               .WithOne(c => c.Applicant)
               .HasConstraintName("FK_One_Applicant_with_Many_Application")
               .HasForeignKey(c => c.ApplicantId)
               .OnDelete(DeleteBehavior.NoAction);

    }
}
