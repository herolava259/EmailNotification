using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Models;

public sealed record FixedWindowCounterSetting(uint LimitSize, TimeSpan Window, TimeSpan TimeoutAwait, uint WindowKeepUnit = 4)
{
    
}
