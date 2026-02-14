using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger;

internal static class TriggerConstantKeys
{
    public const string CorrelationIdKey = "X-Correlation-Id";

    public const string ServiceIdKey = "X-Service-Id";

    public const string ProducerIdKey = "X-Producer-Id";

    public const string SourceTypeKey = "X-Source-Type";
}
