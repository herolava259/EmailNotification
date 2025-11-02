using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Identity.Core.Account.Request;

public record RegisterRequest(string UserName, string Password, string identitySession)
{

}
