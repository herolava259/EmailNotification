using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Core;

public sealed class BookSectionEntity: EntityBase
{
    public string Title { get; set; }

    public uint NoOfChapter { get; set; }

    public string Summary { get; set; }


}
