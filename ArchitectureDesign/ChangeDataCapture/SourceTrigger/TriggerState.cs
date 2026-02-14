using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger;

public sealed class TriggerState
{
    public string CorrelationId { get; set; } = Guid.Empty.ToString();

    public TriggerSourceType SourceType { get; set; }

    public TriggerType Type { get; set; }

    public string AgentId { get; set; } = Guid.Empty.ToString();
    public static TriggerState Default { get => @default;}

    private static TriggerState @default = new()
    { AgentId = string.Empty, CorrelationId = Guid.Empty.ToString(), SourceType = TriggerSourceType.None, Type = TriggerType.None};
}
