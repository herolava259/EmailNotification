

using NotificationSystem.Models;

namespace NotificationSystem.Interfaces;

public interface IProducer<T>: IAsyncDisposable
{
    ValueTask Publish(Event<T> @event, CancellationToken token = default);
}
