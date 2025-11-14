using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Core;

public class ReviewEntity: EntityBase
{
    public int Rating { get; set; }

    public string Content { get; set; }

    public Guid UserId { get; set; }

    public Guid BookId { get; set; }


}
