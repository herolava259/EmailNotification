using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArchitechtureDesign.Generic.EventSourcing.RetroactiveEvent;

public interface IEventAuditor
{
    Task AuditAsync();
}
