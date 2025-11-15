using Playground.Application.Example.Kafka.Core.OrderDomain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.OrderDomain;

public class OrderAggregate: EntityBase, IAggregateRoot
{
    public OrderAggregate()
    {
        
    }


    public OrderAggregate(string orderNo,
                          DateTimeOffset orderDate,
                          Guid userId,
                          string customerEmail,
                          AddressEntity shipToAddress,
                          DeliveryMethodEntity deliveryMethod,
                          IReadOnlyList<LineItemEntity> orderItems,
                          decimal subtotal,
                          string paymentIntentId)
    {
        OrderNo = orderNo;
        OrderDate = orderDate;
        UserId = userId;
        CustomerEmail = customerEmail;
        ShipToAddres = shipToAddress;
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Subtotal = subtotal;
        PaymentIntentId = paymentIntentId;
    }

    public string OrderNo { get; private init; } = "001";

    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.UtcNow;

    public Guid UserId { get; set; }


    public string CustomerEmail { get; set; }

    public AddressEntity ShipToAddres { get; set; }

    public DeliveryMethodEntity DeliveryMethod { get; set; }


    public IReadOnlyList<LineItemEntity> OrderItems { get; set; }

    public decimal Subtotal { get; set; }

    public EOrderStatus Status { get; set; }

    public string PaymentIntentId { get; set; }

    public decimal TotalCost
        => Subtotal + DeliveryMethod.TotalPrice;


}
