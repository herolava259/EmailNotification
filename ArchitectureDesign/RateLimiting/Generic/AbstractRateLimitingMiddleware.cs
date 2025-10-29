using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Generic;

public abstract class AbstractRateLimitingMiddleware
{
    protected readonly RequestDelegate _next;


    public abstract Task InvokeAsync(HttpContext context);

}
