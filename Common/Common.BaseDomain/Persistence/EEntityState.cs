
namespace Common.BaseDomain.Persistence;

public enum EEntityState
{
    Create = 100_000_000,
    Modified = 100_000_001,
    Deleted = 100_000_002,
    NoAction = 100_000_003
}
