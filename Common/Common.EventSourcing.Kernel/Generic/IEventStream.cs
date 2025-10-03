using Common.EventSourcing.Kernel.Core;

namespace Common.EventSourcing.Kernel.Generic;


public interface IEventStream: IAsyncDisposable, IDisposable, IQueryable
{
    public bool CanSeek { get; }
    public bool CanRead { get; }

    public bool CanWrite { get; }

    public ulong Length { get; }

    public ulong WindowSize { get; set; }

    public ulong Position { get; set; }


    public bool JumptoAsync();

    Task<IEnumerable<IStreamEvent>> ReadAsync();

    Task AppendAsync(IEnumerable<IStreamEvent> events);

    Task AppendAsync(IStreamEvent @event);

    Task WirteAsyncIntoEvent(string eventId, object data);


    IQueryable<TEventStream> AsQueryable<TEventStream>()
        where TEventStream: IEventStream;
}

public interface IEventStreamBuilder<TEventStream>: IQueryable<TEventStream>
    where TEventStream: IEventStream
{
    IEventStreamBuilder<TEventStream> HasAggreagteId(string aggregateId);

    IEventStreamBuilder<TEventStream> From(string eventId);

    IEventStreamBuilder<TEventStream> To(string eventId);

    IEventStreamBuilder<TEventStream> ShouldReverse();



    Task<TEventStream> Build();
}
