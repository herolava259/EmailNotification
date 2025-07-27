

namespace Common.Domain.Generic.EventSourcing;

public enum ESourcingEventState: ushort
{
    None = 0, 
    Added = 1,
    Commited = 2,
    Uncommited = 3,
}
