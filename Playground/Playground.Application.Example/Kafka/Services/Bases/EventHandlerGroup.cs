using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Playground.Application.Example.Kafka.Interfaces;
using Playground.Application.Example.Kafka.Services.IntergrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Services.Bases;

public sealed class EventHandlerGroup<TEvent> : BaseEventHandler<TEvent>
	where TEvent: BaseIntergrationEvent
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly ILogger<EventHandlerGroup<TEvent>> _logger;
	private CancellationTokenSource? _tokenSource;

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

	}

	internal async ValueTask StartProcessing(List<IExecutionHandler<TEvent>> eventHandlers, CancellationToken cancellationToken)
	{
		// TODO: implement how it work later 
	}
}
