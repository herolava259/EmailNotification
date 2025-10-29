using RateLimiting.Generic;
using RateLimiting.Models;

namespace RateLimiting.Implementations;

public class TokenBucketSource : ITokenBucketSource
{
    private readonly TokenBucketSetting _setting;

    private uint _numOfToken = 0;

    private DateTime _lastRevision;

    private readonly Lock _lockObj = new();

    public TokenBucketSource(TokenBucketSetting setting)
    {
        _setting = setting;
        _numOfToken = _setting.BucketSize;
        _lastRevision = DateTime.Now;

    }
    public uint TotalToken { get => _numOfToken; }
    public DateTime LastRefill { get => _lastRevision; }


    public void AutoRevision()
    {
        var now = DateTime.Now;
        if((now - _lastRevision) >= _setting.RevisionPeriod)
        {
            _lockObj.Enter();
            try
            {
                _lastRevision = now;
                Interlocked.Exchange(ref _numOfToken, _setting.BucketSize);
            }
            finally
            {
                _lockObj.Exit();
            }
        }
    }

    public bool RetryToken()
    {
        if(_setting.RevisionType == RevisionBucketType.Lazy)
            AutoRevision();

        if (_numOfToken <= 0)
            return false;

        Interlocked.Decrement(ref _numOfToken);

        return true;
    }

    public void Refresh()
    {
        using(_lockObj.EnterScope())
        {
            _lastRevision = DateTime.Now;
            Interlocked.Exchange(ref _numOfToken, _setting.BucketSize);
        }
    }
}
