using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiting.Generic;

public interface IRateLimitingStrategy
{
    public Task<bool> VerifyRequest(HttpContext httpContext);
}
