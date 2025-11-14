using RateLimiting.Generic;
using RateLimiting.Models;

namespace RateLimiting.Implementations;




public sealed class FixedWindowCounter : IFixedWindowCounter
{

    private sealed class TimelineWindow
    {
        public DateTime TimeStart  { get; init; }

        public uint Counter { get => _counter; }

        private uint _counter = 0;

        private readonly Mutex _mutex = new Mutex();

        public TimelineWindow? Next { get; set; }

        public bool Increment(TimeSpan timeoutAwait, uint limit)
        {
            bool incremented = false;

            if(_mutex.WaitOne(timeoutAwait) && Counter < limit)
            {
                Interlocked.Increment(ref _counter);
                incremented = true;
                _mutex.ReleaseMutex();
            }

            return incremented;


        }

        public bool AcceptEvent(IncomingEvent @event, TimeSpan windowLength)
            => @event.TimeStamp >= TimeStart && @event.TimeStamp <= TimeStart + windowLength;

        public void AddNextWindow(TimeSpan interval, DateTime overlappedTime)
        {
            if (Next != null)
            {
                Next.AddNextWindow(interval, overlappedTime);
                return;
            }
            var curWindow = this;

            while(overlappedTime > curWindow.TimeStart + interval)
            {
                curWindow.Next = new TimelineWindow { TimeStart = TimeStart + interval };

                curWindow = curWindow.Next;
            }

        }
    }

    private readonly FixedWindowCounterSetting _setting;
    private TimelineWindow? _head = null;
    private readonly Lock _lock = new Lock();

    public DateTime TimeStart { get; set; }

    public FixedWindowCounter(FixedWindowCounterSetting setting)
    {
        _setting = setting;

    }

    public void Start(DateTime? timeStart = null)
    {
        TimeStart = timeStart ?? DateTime.UtcNow;

        _head = new TimelineWindow
        {
            TimeStart = TimeStart,

        };
    }


    // multi threading
    public bool ForwardRequest(IncomingEvent @event)
    {
        var curWindow = _head;

        IncreaseWindow(@event.TimeStamp);

        while (curWindow != null && !curWindow.AcceptEvent(@event, _setting.Window))
            curWindow = curWindow.Next;

        if (curWindow == null)
            return false;

        if (curWindow.Increment(_setting.TimeoutAwait, _setting.LimitSize))
            return true;

        return false;

    }
    // lazy or cron job doing 
    public void IncreaseWindow(DateTime? timeStone = null)
    {
        timeStone ??= DateTime.UtcNow;

        if(_lock.TryEnter(100))
        {
            var tail = PruneWindowThenGetLast();
            if(tail == null)
            {
                _head = new TimelineWindow { TimeStart = timeStone.Value };
            }
            else
            {
                tail.AddNextWindow(_setting.Window, timeStone.Value );
            }
             _lock.Exit();
        }

    }

    private TimelineWindow? PruneWindowThenGetLast()
    {
        if (_head == null)
            return null;
        int counter = 1;

        var curWindow = _head;

        while(curWindow.Next != null)
        {
            counter += 1;
            curWindow = curWindow.Next;
        }



        if (counter <= _setting.WindowKeepUnit - 1)
            return curWindow;

        while(counter-- > _setting.WindowKeepUnit - 1)
        {
            _head = _head!.Next;
        }

        //GC.Collect();

        return curWindow;
    }
}
