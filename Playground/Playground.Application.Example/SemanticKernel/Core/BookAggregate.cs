using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Core;



public sealed class BookAggregate: EntityBase, IAggregateRoot
{
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public string Title { get; set; } = String.Empty;

    public string Author { get; set; } = String.Empty;

    public DateTimeOffset PublishedDate { get; set; }

    public string Publisher { get; set; } = String.Empty;

    public string Language { get; set; } = String.Empty;

}


