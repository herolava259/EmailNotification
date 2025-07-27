using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.EventSourcing;

public static class EventSourcingConstants
{
    public const string VersionBranchFormat = @"^((V([0-9]+)-)*)V([0-9]+)$";

}
