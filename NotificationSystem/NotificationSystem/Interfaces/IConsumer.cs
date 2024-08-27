

namespace NotificationSystem.Interfaces;

public interface IConsumer : IAsyncDisposable
{
    ValueTask Start(CancellationToken token = default);
    ValueTask Stop(CancellationToken token = default);
}

public interface IConsumer<T>: IConsumer
{ }
