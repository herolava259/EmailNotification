using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Identity.Core.Account.Request;

public sealed record ChangeAccountRequest(IDictionary<string, object> PropertyMapping)
{
}
