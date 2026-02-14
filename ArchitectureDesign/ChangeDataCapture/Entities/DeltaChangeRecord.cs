using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Entities;


public enum ChangeType: ushort
{
    Updated = 0,
    Deleted = 1,
    Created = 2,
}

public sealed record PropertyChange
{
    public string FieldName { get; set; } = "Title";


    public object? BeforeValue { get; set; }

    public object? AfterValue { get; set; }
}

internal sealed record DeltaChangeRecord
{
    public string EntityId { get; init; } = Guid.Empty.ToString();

    public string CorrelationId { get; init; } = Guid.Empty.ToString();// = Entity. 

    public AgentType CorrelationType { get; init; } // = Entity.AgentType

    public IReadOnlyDictionary<string, object> FieldChanges { get; private init; } = new Dictionary<string, object>();

    public long TimeStamp { get; init; } = DateTime.MaxValue.Ticks;

    public string EntityType { get; init; } = "EntityBase";

    public ChangeType Type { get; init; } = ChangeType.Updated;

}
