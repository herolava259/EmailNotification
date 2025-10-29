namespace RateLimiting.Generic;

public interface ITokenBucketSource
{
    public uint TotalToken { get;}

    public DateTime LastRefill { get;}

    public void AutoRevision();

    public bool RetryToken();

    public void Refresh();
}
