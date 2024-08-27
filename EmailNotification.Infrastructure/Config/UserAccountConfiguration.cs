using EmailNotification.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EmailNotification.Infrastructure.Config;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("UserAccount");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.CreatedDate).IsRequired();
        builder.Property(c => c.UpdatedDate).IsRequired();

        builder.Property(c => c.Email).IsRequired().HasMaxLength(256);

        builder.Property(c => c.Status)
                    .HasConversion<int>();
        builder.Property(c => c.LastUpdatePassword).IsRequired();

        builder.HasOne(c => c.Profile)
               .WithOne(c => c.UserAccount)
               .HasForeignKey<Profile>(c => c.UserAccountId)
               .IsRequired()
               .HasConstraintName("FK_One_UserAccount_One_Profile");

    }
}
