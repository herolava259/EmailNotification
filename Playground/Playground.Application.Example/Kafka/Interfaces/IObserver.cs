using Playground.Application.Example.Kafka.Services.IntergrationEvents;

namespace Playground.Application.Example.Kafka.Interfaces;

public interface IObserver
{
}

public interface IObserver<TEvent>: IObserver, IAsyncDisposable
    where TEvent : BaseIntergrationEvent
{
    void Subscribe(ISubscriable<TEvent> subject);
}