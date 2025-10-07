using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Events;

public class OrderProcessInitializationEvent
{
    public Guid OrderId { get; set; }

    [ModuleInitializer]
    internal static void Init()
    {
        GlobalTopology.Send.UseCorrelationId<OrderProcessInitializationEvent>(x => x.OrderId);
    }
}
