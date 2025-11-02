using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Models;

public sealed record SlidingWindowLogSetting(TimeSpan TimeWindow, uint Limit, ulong TimeoutMillisecond=1000)
{
}
