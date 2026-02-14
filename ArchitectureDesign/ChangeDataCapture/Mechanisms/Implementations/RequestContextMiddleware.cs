using ChangeDataCapture.Mechanisms.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Mechanisms.Implementations;

internal class RequestContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRequestContext _requestContext;

    public RequestContextMiddleware(RequestDelegate next, IRequestContext context)
    {
        _next = next;
        _requestContext = context;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // before 
        _requestContext.Initialize(sourceType: ExecutionSource.HttpApi,
                                   userId: context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                                   sourceId: context.Request.Headers["X-Trigger-Service"].FirstOrDefault() ?? string.Empty,
                                   correlationId: context.Request.Headers["X-Correlation-Id"]
                                                                    .FirstOrDefault()
                                                                    ?? Guid.NewGuid().ToString());

        await _next(context);

        //after
    }
}
