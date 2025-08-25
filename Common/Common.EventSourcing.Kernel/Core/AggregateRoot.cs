using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Core;

public abstract class AggregateRoot
{
    public Guid Id { get; set; }

    public BranchVersion Version { get; private set; } = BranchVersion.Empty;

    public abstract bool ApplyEvent<TStreamEvent>(TStreamEvent @event)
        where TStreamEvent : BaseStreamEvent;



}
