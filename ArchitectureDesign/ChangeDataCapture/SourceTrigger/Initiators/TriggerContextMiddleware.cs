using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger.Initiators;



internal class TriggerContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITriggerContext _triggerContext;

    public TriggerContextMiddleware(RequestDelegate next, ITriggerContext triggerContext)
    {
        _next = next;
        _triggerContext = triggerContext;
    }

    public async Task Invoke(HttpContext context)
    {
        string corrId = context.Request
                                .Headers?[TriggerConstantKeys.CorrelationIdKey].FirstOrDefault() ?? Guid.NewGuid().ToString();
        _triggerContext.Set(TriggerConstantKeys.CorrelationIdKey, corrId);

        await _next(context);

    }
}
