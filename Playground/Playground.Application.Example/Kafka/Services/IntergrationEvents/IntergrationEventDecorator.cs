namespace Playground.Application.Example.Kafka.Services.IntergrationEvents;


public interface IEventDecorator<TEvent>
    where TEvent:BaseIntergrationEvent 
{
    public TEvent? Event { get; }
}

// like Matryoshka 
public sealed record EventWrapperRoot<TEvent> : IEventDecorator<TEvent>
    where TEvent : BaseIntergrationEvent
{
    public EventWrapperRoot(TEvent @event)
    {
        Event = @event;
    }

    public EventWrapperRoot()
    {
        
    }
    public TEvent? Event { get; init; }

    public static readonly EventWrapperRoot<TEvent> Empty = new();
        
}

public abstract record IntergrationEventDecorator<TEvent> : IEventDecorator<TEvent>
    where TEvent : BaseIntergrationEvent
{
    public IEventDecorator<TEvent> EventWrapperChild { get; protected init; } =  EventWrapperRoot<TEvent>.Empty;
    public virtual TEvent? Event => EventWrapperChild.Event;
}

public sealed record RoutingEventDecorator<TEvent>: IntergrationEventDecorator<TEvent>
    where TEvent: BaseIntergrationEvent
{
    public string RoutingKey { get; private init; } = "/default";

    public RoutingEventDecorator(TEvent @event, string routingKey = "/default")
    {
        EventWrapperChild = new EventWrapperRoot<TEvent>(@event);
        RoutingKey = routingKey;
    }

    public RoutingEventDecorator(IEventDecorator<TEvent> wrapper, string routingKey)
    {
        EventWrapperChild = wrapper;
        RoutingKey=routingKey;
    }
}

public static class EventDecoratorExtensions
{
    public static RoutingEventDecorator<TEvent> Decorate<TEvent>(this RoutingEventDecorator<TEvent> decorator, string routingKey)
        where TEvent : BaseIntergrationEvent
        => new RoutingEventDecorator<TEvent>(decorator, routingKey);
}
