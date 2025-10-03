using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Transactions;

public enum IsolationLevel: ushort
{
    ReadUncommitted = 0,
    ReadCommited = 1,
    Repeatable = 2,
    Serializable = 3
}
