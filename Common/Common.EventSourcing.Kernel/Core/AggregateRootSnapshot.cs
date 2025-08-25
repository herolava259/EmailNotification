using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Core;

public interface IBoundedContext
{
    public void Apply<TStreamEvent>(TStreamEvent @event)
        where TStreamEvent : StreamEvent;
}





public abstract class AggregateSnapshot<TBoundedContext>
    where TBoundedContext: IBoundedContext
{
    public StreamEvent? CurrentEvent { get; set; }

    public BranchVersion Version { get; private set; } = BranchVersion.Empty;

    public abstract TBoundedContext AggregateRoot { get; }



}
