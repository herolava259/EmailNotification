namespace Common.EventSourcing.Kernel.Core;

public enum BranchPointType
{
    Parallel = 0,
    Stale = 1,
    Effective = 2,
    Current = 3
}
/*
 * What is a BranchPoint?
 * 
 */
public sealed partial class BranchPoint
{
    public Guid Id { get; set; }

    public string EntityType { get; set; } = String.Empty;

    public string AggregateId { get; set; } = String.Empty;

    public ulong VersionCounter { get; set; } = 0;

    public ulong PrevBranchCounter { get; set; } = 0; 

    public Guid? ParentBranchPointId { get; set; }

    public BranchPointType BranchPointType { get; set; } = BranchPointType.Effective;

}


public sealed partial class BranchPoint
{
    public BranchPoint? Parent { get; set; }


    private readonly List<BranchPoint> _ancestors = new List<BranchPoint>();
    public IReadOnlyList<BranchPoint> Ancestors { get => _ancestors;  }

}