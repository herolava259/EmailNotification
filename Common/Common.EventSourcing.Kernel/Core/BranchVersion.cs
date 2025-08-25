using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Core;

public record BranchVersion
{
    // example: [branchId].1->0.99->0.1000->4.100
    public string PointChain { get; init; } = String.Empty;

    public ulong PointVersion { get; protected init; } = 0;

    public static BranchVersion Empty
        => new BranchVersion($"[#].[#]->{Guid.Empty}.?", 0);


    private BranchVersion(string branchPointChain, ulong branchPointVersion)
    {
        PointChain = branchPointChain;
  
        PointVersion = branchPointVersion;

    }

    public BranchVersion Next
        => new BranchVersion(this.PointChain, this.PointVersion + 1);

    public BranchVersion? Previous
    {
        get
        {
            int lastPointer = PointChain.LastIndexOf("->") - 1;
            if (lastPointer <= 0)
                return BranchVersion.Empty;

            var startPointer = PointChain.LastIndexOf('.', 0, lastPointer);

            if (startPointer <= 0)
                return BranchVersion.Empty;

            ulong pointVersion = ulong.Parse(PointChain.Substring(startPointer+1, lastPointer-startPointer));

            lastPointer = PointChain.LastIndexOf("->",0, startPointer);

            return new BranchVersion($"{PointChain.Substring(0, lastPointer)}.?", pointVersion);
        }
    }

    public BranchVersion Branching
        => new BranchVersion($"{this.PointChain.Replace("?", PointVersion.ToString())}->[{Guid.NewGuid()}].?", 0);


    public IEnumerable<Tuple<Guid, ulong>> AncestorPointChain
        => PointChain.Split("->").SkipLast(1).Select(bv =>
        {
            var branchAndVersion = bv.Split('.');

            var id = new Guid(branchAndVersion[0].Trim('[', ']'));

            var version = ulong.Parse(branchAndVersion[1]);


            return new Tuple<Guid, ulong>(id, version);
        });

    public static BranchVersion NewBranch()
        => new BranchVersion($"[{Guid.NewGuid()}].?", 0);
}
