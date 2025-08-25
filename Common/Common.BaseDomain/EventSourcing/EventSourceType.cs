using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.EventSourcing
{
    public enum EventSourceType: ushort
    {
        Trigger = 0,
        Request = 1,
        Schedule = 2,
        Consequence = 3,

    }
}
