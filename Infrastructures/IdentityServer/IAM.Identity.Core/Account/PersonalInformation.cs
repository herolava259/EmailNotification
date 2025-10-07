using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Identity.Core.Account;

public sealed record PersonalInformation(string Email, string LastName, string FirstName, string PhoneNumber, string? Address)
{
}
