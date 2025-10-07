using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Identity.Core.Account;



public sealed class ChangeAccountEvent
{
    public IDictionary<string, object> PropertyMapping { get; set; } = new Dictionary<string, object>();
}
