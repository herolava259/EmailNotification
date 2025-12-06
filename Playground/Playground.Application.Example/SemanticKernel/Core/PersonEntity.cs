using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Core;

public abstract class PersonEntity: EntityBase
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Surname { get; set; }


    public ICollection<string> Hobbies { get; set; }

    public string Address { get; set; }


}
