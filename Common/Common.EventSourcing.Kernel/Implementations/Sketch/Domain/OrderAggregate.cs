using Common.EventSourcing.Kernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Implementations.Sketch.Domain;




internal class OrderAggregate : AggregateRoot
{

    public IList<LineItemEntity> Items { get; set; } = new List<LineItemEntity>();


    public OrderStatus Status { get; set; }

    public string Address { get; set; }


    public decimal TotalPrice { get; set; }


    // implementation behaviours 

    public override TResult Apply<TEvent, TResult>(TEvent @event)
    {
        throw new NotImplementedException();
    }

    public override void Apply<TDomainEvent>(TDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}
