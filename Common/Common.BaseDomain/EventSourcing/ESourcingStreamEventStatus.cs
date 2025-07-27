using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.EventSourcing;

public enum ESourcingStreamEventStatus: ushort
{
   
   Current = 0,
   Stale = 1,
   Prepare = 2
}
