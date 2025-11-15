using Microsoft.AspNetCore.Mvc.Filters;
using Playground.API.Extensions;
using Playground.Application.Example.Kafka.Core.Repositories.UserContext;

namespace Playground.API.Filters;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (context.HttpContext.User.Identity?.IsAuthenticated is false)
            return;

        var userId = resultContext.HttpContext.User.GetUserId();

        var uow = resultContext.HttpContext.RequestServices.GetRequiredService<IUserUnitOfWork>();

        var user = await uow.UserRepository.FindAsync(userId);

        if (user == null) return;

        user.LastActive = DateTimeOffset.UtcNow;

        await uow.CompleteAsync();

    }
}
