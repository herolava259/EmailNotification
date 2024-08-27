

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using System.Threading.Channels;

namespace NotificationSystem.Behaviours;

internal sealed class InMemoryEventBusConsumer<T> : IConsumer<T>
{
    private readonly ChannelReader<Event<T>> _bus;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<InMemoryEventBusConsumer<T>> _logger;
    private CancellationTokenSource? _stoppingToken;

    public InMemoryEventBusConsumer(ChannelReader<Event<T>> bus,
                                    IServiceScopeFactory scopeFactory,
                                    ILogger<InMemoryEventBusConsumer<T>> logger)
    {
        this._bus = bus;
        this._scopeFactory = scopeFactory;
        this._logger = logger;
    }

    private void EnsureStoppingTokenIsCreated(CancellationToken token = default)
    {
        if (_stoppingToken is not null && !_stoppingToken.IsCancellationRequested)
        {
            _stoppingToken.Cancel();
        }

        _stoppingToken = token.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(token) : new CancellationTokenSource();
    }

    public ValueTask DisposeAsync()
    {
        _stoppingToken?.Cancel();

        return ValueTask.CompletedTask;
    }

    public async ValueTask Start(CancellationToken token = default)
    {
        EnsureStoppingTokenIsCreated(token);

        await using var scope = _scopeFactory.CreateAsyncScope();

        var handlers = scope.ServiceProvider.GetServices<IEventHandler<T>>().ToList();

        var contextAccessor = scope.ServiceProvider
                                    .GetRequiredService<IEventContextAccessor<T>>();

        if(!handlers.Any())
        {
            _logger.LogDebug("No handlers defined for event of {type}", typeof(T).Name);
            return;
        }

        Task.Run(
            async () => await StartProcessing(handlers, contextAccessor).ConfigureAwait(false),
            _stoppingToken!.Token
        ).ConfigureAwait(false);
    }

    internal async ValueTask StartProcessing(List<IEventHandler<T>> handlers,
                                            IEventContextAccessor<T> contextAccessor)
    {
        var continuousChannelIterator = _bus.ReadAllAsync(_stoppingToken.Token)
                                            .WithCancellation(_stoppingToken.Token)
                                            .ConfigureAwait(false);

        await foreach(var task in continuousChannelIterator)
        {

            if (_stoppingToken.IsCancellationRequested)
                break;

            await Parallel.ForEachAsync(handlers, _stoppingToken.Token,
                            async (handler, scopedToken) =>
                                await ExcuteHandler(handler, task, contextAccessor, scopedToken)
                                    .ConfigureAwait(false))
                           .ConfigureAwait(false);
        }


    }

    internal ValueTask ExcuteHandler(IEventHandler<T> handler, Event<T> task, IEventContextAccessor<T> ctx, CancellationToken scopedToken)
    {
        ctx.Set(task);

        using var logScope = _logger.BeginScope(task.Metadata ?? new EventMetadata(Guid.NewGuid().ToString()));

        Task.Run(async () => await handler
                            .Handle(task.Data, scopedToken), scopedToken)
                .ConfigureAwait(false);

        return ValueTask.CompletedTask;
    }

    public async ValueTask Stop(CancellationToken token = default)
    {
        await DisposeAsync().ConfigureAwait(false);
    }
}
