using Common.EventSourcing.Kernel.Core;

namespace Common.EventSourcing.Kernel.Generic;


public interface IEventStream: IAsyncDisposable, IDisposable, ICloneable
{
    public bool CanSeek { get; }
    public bool CanRead { get; }

    public bool CanWrite { get; }

    public ulong Length { get; }


    Task<IEnumerable<StreamEvent>> ReadAsync();

    Task WriteAsync(IEnumerable<StreamEvent> events);

    Task WriteAsync(StreamEvent @event);
}

public interface IEventStreamBuilder<TEventStream>
    where TEventStream: IEventStream
{
    Task<TEventStream> BuildAsync();
}
