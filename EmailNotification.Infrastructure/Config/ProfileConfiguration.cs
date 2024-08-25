

using EmailNotification.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailNotification.Infrastructure.Config;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profile");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.CreatedDate).IsRequired();
        builder.Property(c => c.UpdatedDate).IsRequired();

        builder.Property(c => c.FirstName)
                    .HasMaxLength(50)
                    .IsRequired()
                    ;
        builder.Property(c => c.LastName)
                    .HasMaxLength(50)
                    .IsRequired()
                    ;
        builder.HasOne(c => c.UserAccount)
               .WithOne(c => c.Profile)
               .HasForeignKey<Profile>(c => c.UserAccountId)
               .IsRequired()
               .HasConstraintName("FK_One_UserAccount_One_Profile");
    }
}
