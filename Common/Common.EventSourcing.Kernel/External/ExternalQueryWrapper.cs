using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.External;

public interface IExternalQuery
{ }
public class ExternalQueryWrapper<TExternalQuery>
    where TExternalQuery : IExternalQuery
{
    public string EventId { get; protected init; }

    public DateTimeOffset TimeStamp { get; set; }

    public TExternalQuery Query { get; protected init; }

    public ExternalQueryWrapper(string eventId, TExternalQuery query)
    {
        EventId = eventId;
        Query = query;
    }

}
