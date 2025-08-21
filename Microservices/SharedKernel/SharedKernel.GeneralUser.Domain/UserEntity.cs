using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.GeneralUser.Domain;

public abstract class UserEntity
{
    public string UserID { get; private init; } = String.Empty;

    public string Name { get; private set; } = String.Empty;

    public string Email { get; set; } = String.Empty;
}
