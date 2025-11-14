using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Core;

public class BookDetailEntity
{
    public Guid BookId { get; set; }
    public int PageCount { get; set; }

    public string ResourceUrl { get; set; } // ex: https://minio...

    public string Format { get; set; } // pdf, word, docx, txt

    public string? ImageUrl { get; set; }

    public ICollection<ReviewEntity>? Reviews { get; set; }

    public ICollection<TagEntity>? Tags { get; set; }


}
