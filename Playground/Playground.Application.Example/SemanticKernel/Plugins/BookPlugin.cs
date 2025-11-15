using AutoMapper;
using DnsClient.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Playground.Application.Example.SemanticKernel.Core.Repositories;
using Playground.Application.Example.SemanticKernel.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Playground.Application.Example.SemanticKernel.Plugins;

public class BookPlugin(IBookRepository _bookRepository, ILogger<BookPlugin> _logger, IMapper _mapper)
{
    [KernelFunction("get_book_by_id")]
    [Description("Gets a list of booka following by name of book")]
    public async Task<List<BookDto>> GetBookDtosByName(string name)
    {
        _logger.LogInformation("Running GetBookDtosByName with name: {name}", name);
        var results = await _bookRepository.GetAllAsyncWithCondition(b => b.Name == name);
        return _mapper.Map<List<BookDto>>(results);
    }



}
