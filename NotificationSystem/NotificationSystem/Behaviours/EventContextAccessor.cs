using NotificationSystem.Interfaces;
using NotificationSystem.Models;


namespace NotificationSystem.Behaviours;

internal class EventContextAccessor<T> : IEventContextAccessor<T>
{
    private static readonly AsyncLocal<EventMetadaWrapper<T>> Holder = new();


    public Event<T>? Event => Holder.Value?.Event;

    public void Set(Event<T> @event)
    {
        var holder = Holder.Value;

        if(holder != null)
        {
            holder.Event = null;
        }

        Holder.Value = new EventMetadaWrapper<T> { Event = @event };
    }
}

internal sealed class EventMetadaWrapper<T>
{
    public Event<T>? Event { get; set; }
}
