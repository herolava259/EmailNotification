using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Config;

public class ListItemFluentConfiguration : IEntityTypeConfiguration<ListItem>
{
    public void Configure(EntityTypeBuilder<ListItem> builder)
    {
        builder.ToTable("ListItem");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CreatedDate).IsRequired();
        builder.Property(c => c.UpdatedDate).IsRequired();

        builder.Property(c => c.ProductId)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(c => c.Amount).IsRequired();

        builder.HasOne(c => c.Cart)
               .WithMany(c => c.ListItems)
               .HasForeignKey(c => c.CartId)
               .IsRequired()
               .HasConstraintName("FK_One_Cart_Many_ListItem");
    }
}
