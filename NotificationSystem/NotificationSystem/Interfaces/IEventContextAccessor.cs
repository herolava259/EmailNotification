

using NotificationSystem.Models;

namespace NotificationSystem.Interfaces;

public interface IEventContextAccessor<T>
{
    public Event<T> Event { get; }

    void Set(Event<T> @event);
}
