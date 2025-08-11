namespace Cart.Domain.Cart;

public sealed partial class CartAggregate
{
    public Guid UserId { get; private init; } = Guid.Empty;

}
