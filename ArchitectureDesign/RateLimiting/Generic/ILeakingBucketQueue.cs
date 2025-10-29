using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Generic;

public interface ILeakingBucketQueue
{
    public (bool, ManualResetEventSlim?) Enqueue(string requestId, CancellationToken ct = default);

    public IEnumerable<string> DequeueThenSignal(int numEvent = 0, CancellationToken ct = default);

}
