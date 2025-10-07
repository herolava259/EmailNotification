using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine;

public class CheckProductStockEvent
{
    public Guid OrderId { get; set; }

    [ModuleInitializer]
    internal static void Init()
    {
        GlobalTopology.Send.UseCorrelationId<CheckProductStockEvent>(x => x.OrderId);
    }
}
