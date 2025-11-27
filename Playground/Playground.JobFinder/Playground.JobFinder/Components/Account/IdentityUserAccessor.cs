using Microsoft.AspNetCore.Identity;
using Playground.JobFinder.Data;

namespace Playground.JobFinder.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<ApplicationAccount> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<ApplicationAccount> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
