using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Core;

// read only model 
public abstract class BaseStreamEvent
{
    public Guid AggregateRootId { get; private init; } = Guid.Empty;

    public string Id { get; init; } = String.Empty;

    public BranchVersion CurrentBranch { get; private init; } =  BranchVersion.Empty;


    public DateTimeOffset OccuredDate { get; set; }

    public DateTimeOffset TimeStamp { get; private init; } = DateTimeOffset.UtcNow;


}
