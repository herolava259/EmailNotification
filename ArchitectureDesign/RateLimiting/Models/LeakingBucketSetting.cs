using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Models;

public enum AcquireTokenType: ushort
{
    Lazy = 0, 
    Eager = 1,
}


public class LeakingBucketSetting
{
    public uint BucketSize { get; set; }

    public uint OutflowRate { get; set; }

    public AcquireTokenType AcquireTokenType { get; set; } = AcquireTokenType.Lazy;

    public int SpinCount { get; set; } = 0;
 }
