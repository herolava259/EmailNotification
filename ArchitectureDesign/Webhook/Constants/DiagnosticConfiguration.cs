using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhook.Constants;

internal static class DiagnosticConfiguration
{
    internal static readonly ActivitySource Source = new("webhooks-api");
}
