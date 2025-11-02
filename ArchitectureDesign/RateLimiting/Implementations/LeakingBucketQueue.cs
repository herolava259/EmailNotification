using Microsoft.Extensions.Logging;
using RateLimiting.Generic;
using RateLimiting.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Implementations;

public class LeakingBucketQueue : ILeakingBucketQueue
{
    private readonly LeakingBucketSetting _settings;

    private readonly ILogger<ILeakingBucketQueue> _logger;


    private readonly ConcurrentQueue<(string, ManualResetEventSlim)> _queue = new();

    private readonly ConcurrentQueue<ManualResetEventSlim> _availableSlots = new();


    public LeakingBucketQueue(LeakingBucketSetting settings, ILoggerFactory loggerFactory)
    {
        _settings = settings;

        foreach(var _ in Enumerable.Range(0, (int)settings.BucketSize))
        {
            _availableSlots.Enqueue(new ManualResetEventSlim(false, settings.SpinCount));
        }

        _logger = loggerFactory.CreateLogger<ILeakingBucketQueue>();
    }

    public IEnumerable<string> DequeueThenSignal(int window = 0, CancellationToken ct = default)
    {
        if (window == 0)
            window = (int)_settings.OutflowRate;

        window = Math.Min(window, _queue.Count);

        var results = new List<string>();

        while(window-- > 0)
        {
            if(_queue.TryDequeue(out var pair)){
                _logger.LogInformation("Raise Request: {requestId}", pair.Item1);
                pair.Item2.Set();
                results.Add(pair.Item1);
                _availableSlots.Enqueue(pair.Item2);
            }
        }

        return results;
    }


    public (bool, ManualResetEventSlim?) Enqueue(string requestId, CancellationToken ct = default)
    {
        if (!_availableSlots.Any())
            return (false, null);

        if(_availableSlots.TryDequeue(out var availSignal))
        {
            availSignal.Reset();
            _logger.LogInformation(message: "Enter request {requestId} to queue.", requestId);

            _queue.Enqueue((requestId, availSignal));

            return (true, availSignal);
        }

        return (false, null);
    }
}
