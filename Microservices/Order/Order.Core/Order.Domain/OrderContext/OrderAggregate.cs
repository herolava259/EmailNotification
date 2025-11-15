namespace Order.Domain.OrderContext;

public sealed partial class OrderAggregate
{
    public string OrderNo { get; private init; } = "001";

    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset EstimatedDeliveryTime { get; set; } = DateTimeOffset.UtcNow;

    public IReadOnlyCollection<LineItemEntity> LineItems { get; set; } = new List<LineItemEntity>();

    public Guid MechantId { get; set; } = Guid.Empty;

    public Guid CustomerId { get; set; }

    public decimal TotalPrice { get; }
}
