using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger.Initiators;

public sealed class ContextInitiatorActivity : IAsyncActionFilter
{
    private static string GetUserId(ActionExecutingContext context)
    {
        var userPrincipal = context.HttpContext.User;

        if (userPrincipal == null) 
            throw new InvalidOperationException("Non identity user in response");

        return userPrincipal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = Guid.Empty.ToString();

        var triggerContext = context.HttpContext.RequestServices.GetRequiredService<ITriggerContext>();

        if (context.HttpContext.User.Identity?.IsAuthenticated is true)
            userId = GetUserId(context);

        triggerContext.Initialize(TriggerSourceType.Client, TriggerType.RestfulApi, userId, triggerContext.Get<string>(TriggerConstantKeys.CorrelationIdKey));

        await next();
    }
}
