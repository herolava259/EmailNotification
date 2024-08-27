

namespace NotificationSystem.Interfaces;

public interface IEventHandler<in T>
{
    ValueTask Handle(T? time, CancellationToken token = default);


}
