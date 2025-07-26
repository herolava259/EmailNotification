
using Cart.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using CartEntity = Cart.Core.Entities.Cart;

namespace Cart.Infrastructure.Config;

public sealed class CartFluentConfiguration : AbstractEntityBaseFluentConfiguration<CartEntity>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartEntity> builder)
    {
        builder.ToTable("Cart");

        base.Configure(builder);

        builder.Property(c => c.TotalPrice).HasPrecision(18, 4) // HasColumnType("decimal(18, 4)")
                                           .IsRequired();

        builder.HasMany(c => c.ListItems)
               .WithOne(c => c.Cart)
               .HasForeignKey(c => c.CartId)
               .IsRequired()
               .HasConstraintName("FK_One_Cart_Many_ListItem");

    }
}
