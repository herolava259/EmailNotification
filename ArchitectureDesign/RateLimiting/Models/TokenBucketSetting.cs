using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Models;


public enum RevisionBucketType: ushort
{
    BackgroundDo = 1,
    Lazy = 2
}

public sealed class TokenBucketSetting
{
    public uint BucketSize { get; set; }

    public TimeSpan RevisionPeriod { get; set; }

    public RevisionBucketType RevisionType { get; set; } = RevisionBucketType.Lazy;
}
