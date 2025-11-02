using IAM.Identity.Core.Account.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Identity.Core.Account.Domain;


public abstract class AccountBase<TKey>
    where TKey: notnull
{
    public Guid Id { get; set; }

    public TKey Key { get; set; }

    public string PasswordHash { get; set; }

    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    public string SecurityStamp { get; set; }

    public PersonalInformation PersonalData { get; set; }


    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset LastChangePassword { get; set; }

    public ICollection<string> PaswordHashRecordHistories { get; set; }

    public ICollection<ChangeAccountRequest> Changes { get; set; }
}
