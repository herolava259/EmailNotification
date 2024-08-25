
using Microsoft.EntityFrameworkCore;
using CartEntity = Cart.Core.Entities.Cart;

namespace Cart.Infrastructure.Config;

public class CartFluentConfiguration : IEntityTypeConfiguration<CartEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartEntity> builder)
    {
        builder.ToTable("Cart");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CreatedDate).IsRequired();
        builder.Property(c => c.UpdatedDate).IsRequired();

        builder.Property(c => c.TotalPrice).IsRequired();

        builder.HasMany(c => c.ListItems)
               .WithOne(c => c.Cart)
               .HasForeignKey(c => c.CartId)
               .IsRequired()
               .HasConstraintName("FK_One_Cart_Many_ListItem");

    }
}
