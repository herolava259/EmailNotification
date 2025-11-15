using System.Security.Claims;

namespace Playground.API.Extensions;

public static class ClaimPrincipleExtension
{
    public static string GetUsername(this ClaimsPrincipal userPrincipal)
    {
        var userName = userPrincipal.FindFirstValue(ClaimTypes.Name)
            ?? throw new InvalidOperationException("Non identity user in response");
        return userName;
    }

    public static Guid GetUserId(this ClaimsPrincipal user)
    {

        if (Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            throw new InvalidOperationException("Cannot get username from token");

        return userId;
    }
}
