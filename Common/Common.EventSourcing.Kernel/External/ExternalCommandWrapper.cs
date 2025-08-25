namespace Common.EventSourcing.Kernel.External;

public interface IExternalCommand
{ }

public class ExternalCommandWrapper<TCommand>
    where TCommand : IExternalCommand
{
    public string EventId { get; protected init; }

    public DateTimeOffset TimeStamp { get; set; }

    public TCommand Command { get; protected init; }

    public ExternalCommandWrapper(string eventId, TCommand command)
    {
        EventId = eventId;
        Command = command;
    }
}
