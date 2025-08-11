using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArchitechtureDesign.Generic.EDA;

public abstract record BaseIntergrationEvent
{
    public string CorrelationId { get; private init; } = String.Empty;


    public DateTimeOffset CreationDate { get; private init; }


   protected BaseIntergrationEvent()
   {
        CorrelationId = Guid.NewGuid().ToString();
        CreationDate = DateTimeOffset.UtcNow;

   }

    protected BaseIntergrationEvent(Guid correlationId, DateTimeOffset? creationDate = null)
    {
        CorrelationId = correlationId.ToString();
        CreationDate = creationDate ?? DateTimeOffset.UtcNow;
    }

}
