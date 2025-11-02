using Microsoft.AspNetCore.Http;
using RateLimiting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Generic;

public interface IFixedWindowCounter
{

    public DateTime TimeStart { get; }


    public bool ForwardRequest(IncomingEvent @event);

    public void IncreaseWindow(DateTime? timeStone = null);
}
