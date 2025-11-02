using RateLimiting.Generic;
using RateLimiting.Models;

namespace RateLimiting.Implementations;

public class SlidingWindowLogLimiter : ISlidingWindowLogLimiter
{
    private sealed class ConcurrentLog: SortedSet<DateTime>
    {
        private readonly Lock _lock = new Lock();



        public bool AddConcurrently(DateTime item,ulong timeoutMillisecond = 1000)
        {
            bool added = false;
            if(_lock.TryEnter(TimeSpan.FromMilliseconds(timeoutMillisecond)))
            {
                added = base.Add(item);
                _lock.Exit();
            }
            return added;
        }

        public int RemoveConcurrentlyWhere(Predicate<DateTime> match, ulong timeoutMillisecond)
        {
            int numRmv = -1;

            if(_lock.TryEnter(TimeSpan.FromMilliseconds(timeoutMillisecond)))
            {
                numRmv = base.RemoveWhere(match);
            }

            return numRmv;

        }

    }

    private readonly ConcurrentLog _log = new();

    private readonly SlidingWindowLogSetting _setting;

    public SlidingWindowLogLimiter(SlidingWindowLogSetting setting)
    {
        this._setting = setting;
    }
    public bool AcceptRequest(IncomingEvent @event)
    {
        var lastTimeDenied = @event.TimeStamp - _setting.TimeWindow;
        
        if(Monitor.TryEnter(_log, TimeSpan.FromMilliseconds(_setting.TimeoutMillisecond)))
        {

            bool state = false;
            _ = _log.RemoveConcurrentlyWhere(c => c < lastTimeDenied, _setting.TimeoutMillisecond);

            _ = _log.AddConcurrently(@event.TimeStamp);

            if (_log.Count() <= _setting.Limit)
                state = true;

            Monitor.Exit(_log);
            return state;
        }
        else
        {
            throw new TimeoutException();
        }

    }
}
