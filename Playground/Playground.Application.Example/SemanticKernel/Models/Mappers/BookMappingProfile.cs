using AutoMapper;
using Playground.Application.Example.SemanticKernel.Core;
using Playground.Application.Example.SemanticKernel.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Models.Mappers;

internal class BookMappingProfile:Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookAggregate, BookDto>().ReverseMap();
    }
}
