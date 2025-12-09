
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Playground.Application.Example.Kafka.Interfaces;
using Playground.Application.Example.Kafka.Services.Bases;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System.Collections.Concurrent;
using System.Threading.Channels;


namespace Playground.Application.Example.Kafka.Services.Abstractions.Advances;


public interface IForwardResult
{ }

public record BaseForwardResult(bool Ack = false, bool Match = false): IForwardResult
{
    public static BaseForwardResult NotMatch()
        => new BaseForwardResult(true, false);

    public static BaseForwardResult Complete()
        => new BaseForwardResult(true, true);

    public static BaseForwardResult InComplete()
        => new BaseForwardResult(false, false);
}

public interface IEventEndpoint<TEvent>
    where TEvent: BaseIntergrationEvent
{
    bool Match(RoutingEventDecorator<TEvent> eventWrapper);

    Task<IForwardResult> ForwardAsync(RoutingEventDecorator<TEvent> eventWrapper, CancellationToken ctk = default); 
}

public sealed class EventPubSubEndpoint<TEvent> : IEventEndpoint<TEvent>, ISubscriable<TEvent>
    where TEvent : BaseIntergrationEvent
{
    //IServiceScopeFactory scopeFactory, 
    public EventPubSubEndpoint(ILogger<EventPubSubEndpoint<TEvent>> logger)
    {
        //this._scopeFactory = scopeFactory;
        this._logger = logger;
    }

    private readonly string _path = "default/";
    //private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<EventPubSubEndpoint<TEvent>> _logger;

    public event Func<TEvent, CancellationToken, Task> OnEventOccurred = static (_, _) => Task.CompletedTask;

    public async Task<IForwardResult> ForwardAsync(RoutingEventDecorator<TEvent> eventWrapper, CancellationToken ctk = default)
    {
        // TODO: implment later 
        // logging 
        // or timing 

        if (!this.Match(eventWrapper) || eventWrapper.Event is null)
            return BaseForwardResult.NotMatch();

        try
        {
            await OnEventOccurred(eventWrapper.Event, ctk);
            return BaseForwardResult.Complete();
        }
        catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BaseForwardResult.InComplete();
        }
    }

    
    public bool Match(RoutingEventDecorator<TEvent> eventWrapper)
        => eventWrapper.Match(_path);
}

public interface IContractHandler<TEvent>: IExecutionHandler<TEvent>
    where TEvent : BaseIntergrationEvent
{ }

public interface IQueueEndpoint<TEvent>: IAsyncDisposable
{
    Task AcceptAsync(TEvent @event);

    Task PreProcessing();
    Task StartProcessing(CancellationToken ctx);

    Task StopProcessing(CancellationToken ctx = default);
}

public sealed class EventQueueEndpoint<TEvent, THandler>: IQueueEndpoint<TEvent>
    where TEvent : BaseIntergrationEvent
    where THandler : IContractHandler<TEvent>
{
    private readonly Channel<TEvent> _channel;

    private CancellationTokenSource? _tokenSource;

    public EventQueueEndpoint(string name, IServiceScopeFactory scopeFactory, ILogger<EventQueueEndpoint<TEvent, THandler>> logger) 
    {
        _channel = Channel.CreateUnbounded<TEvent>();
    }

    private void EnsureStoppingTokenIsCreated(CancellationToken token = default)
    {
        if (_tokenSource is not null && !_tokenSource.IsCancellationRequested)
        {
            _tokenSource.Cancel();
        }

        _tokenSource = token.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(token) : new CancellationTokenSource();
    }

    public Task AcceptAsync(TEvent @event)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task StartProcessing(CancellationToken ctx)
    {
        throw new NotImplementedException();
    }

    public Task StopProcessing(CancellationToken ctx = default)
    {
        throw new NotImplementedException();
    }

    public Task PreProcessing()
    {
        throw new NotImplementedException();
    }
}


public class EventAggregatorEndpoint
{
    
}
