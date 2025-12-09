using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Playground.Application.Example.Kafka.Interfaces;
using Playground.Application.Example.Kafka.Services.Abstractions.Bases;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;

namespace Playground.Application.Example.Kafka.Services.Bases;

public class EventHandlerGroup<TEvent> : BaseEventHandler<TEvent>
	where TEvent: BaseIntergrationEvent
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly ILogger<EventHandlerGroup<TEvent>> _logger;
	protected CancellationTokenSource? _tokenSource;

    public string Name { get; private init; }

    public EventHandlerGroup(string name, IServiceScopeFactory scopeFactory, ILogger<EventHandlerGroup<TEvent>> logger)
    {
        this._scopeFactory = scopeFactory;
		this._logger = logger;
		Name = name;
    }

	private void EnsureStoppingTokenIsCreated(CancellationToken token = default)
	{
		if (_tokenSource is not null && !_tokenSource.IsCancellationRequested)
		{
			_tokenSource.Cancel();
		}

		_tokenSource = token.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(token) : new CancellationTokenSource();
	}


	public override async Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default)
    {
		await using var scope = _scopeFactory.CreateAsyncScope();

		var handlers = scope.ServiceProvider.GetServices<IExecutionHandler<TEvent>>().ToList();

		await StartProcessing(handlers,@event, cancellationToken);

	}

	internal virtual async ValueTask StartProcessing(List<IExecutionHandler<TEvent>> eventHandlers,TEvent @event, CancellationToken cancellationToken)
	{
		// TODO: implement how it work later 

		EnsureStoppingTokenIsCreated(cancellationToken);

		if (!eventHandlers.Any())
		{
			_logger.LogDebug("No handlers defined for event of {type}", typeof(TEvent).Name);
			return;
		}

		await Parallel.ForEachAsync(eventHandlers, _tokenSource!.Token,
									async (handler, scopedToken) => 
										await handler.ExecuteAsync(@event, cancellationToken))
					  .ConfigureAwait(false);

	}


	internal virtual async ValueTask StopProcessing(CancellationToken token = default)
	{
		await DisposeAsync().ConfigureAwait(false);
	}

    public override async ValueTask DisposeAsync()
    {
		if (_tokenSource is null)
			return;
		await _tokenSource.CancelAsync();
		_tokenSource?.Dispose();

    }
}
