using Microsoft.AspNetCore.Identity;
using Scheduler.Web.Data;
using Scheduler.Web.Entities;

namespace Scheduler.Web.Features;

public static class RegisterUser
{
    public record Request(string Email, string Initials, string Password, bool EnableNotifications = false);

    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/register", async (Request request,
                                              ApplicationDbContext dbContext,
                                              UserManager<ApplicationUser> userManager) =>
        {

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Initials = request.Initials,
                EnableNotifications = request.EnableNotifications
            };
            var result = await userManager.CreateAsync(user, request.Password);


            if (!result.Succeeded)
                return Results.BadRequest(result.Errors);

            var addRoleResult = await userManager.AddToRoleAsync(user, Roles.Member);

            if(!addRoleResult.Succeeded)
                return Results.BadRequest(addRoleResult.Errors);

            await transaction.CommitAsync();

            return Results.Ok(user);
        })
        .WithName("RegisterUser")
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem();
    }
}
