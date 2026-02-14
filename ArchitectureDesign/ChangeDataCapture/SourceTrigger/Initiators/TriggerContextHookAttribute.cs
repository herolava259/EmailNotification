using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
namespace ChangeDataCapture.SourceTrigger.Initiators;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TriggerContextHookAttribute(IHttpContextAccessor _httpContextAccessor,
                                       ITriggerContext _triggerContext): ActionFilterAttribute
{
    private static string GetUserId(HttpContext context)
    {
        if (context.User.Identity is null || !context.User.Identity.IsAuthenticated)
            return Guid.Empty.ToString();

        var userPrincipal = context.User;

        if (userPrincipal == null)
            throw new InvalidOperationException("Non identity user in response");

        return userPrincipal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
    }
    public override void OnActionExecuting(HttpActionContext actionContext)
    {

        var corrId = actionContext.Request.Headers.Where(p => p.Key == TriggerConstantKeys.CorrelationIdKey)
                                                   .SelectMany(p => p.Value)
                                                   .SingleOrDefault(Guid.Empty.ToString());

        var userId = GetUserId(_httpContextAccessor.HttpContext);

        

        _triggerContext.Initialize(TriggerSourceType.Client, TriggerType.RestfulApi, userId, corrId);
        base.OnActionExecuting(actionContext);
    }
  
}
