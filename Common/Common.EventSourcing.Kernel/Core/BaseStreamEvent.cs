namespace Common.EventSourcing.Kernel.Core;


public enum SourcingEventState: ushort
{
    PrimaryActive = 0,
    SecondaryActive = 1,
    Corrected = 2,
    Duplicated = 3,
    Rejected = 4,
    Ignored = 5

}
public abstract class BaseStreamEvent
{
    public string IdempotenceKey { get; set; } = String.Empty;
    public Guid AggregateRootId { get; set; }

    public Guid BranchPointId { get; set; }

    public DateTimeOffset OccuredOn { get; set; }

    public DateTimeOffset TimeStamp { get; set; }

    public SourcingEventState State { get; set; } = SourcingEventState.PrimaryActive;

    public ulong VersionNumber { get; set; } = 0;


}
