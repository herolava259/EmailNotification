using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Playground.Application.Example.SemanticKernel.Core;
using Playground.Application.Example.SemanticKernel.Storage.SqlLite;

namespace Playground.API.Endpoints;

public static class BookStoreApiEndpoints
{
    public sealed record UpdateDescriptionBooModel(Guid BookId, string Description)
    { }
    public static IEndpointRouteBuilder MapBookStoreEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("book/update/description", ([FromBody] UpdateDescriptionBooModel bookUpdate,BookLibraryDbContext dbContext, ILogger<Program> logger) =>
        {
            var book = dbContext.BookDomains.Find(bookUpdate.BookId);

            if (book == null)
            {
                logger.LogWarning("Invalid book id {BookId}", bookUpdate.BookId);
                return Results.BadRequest($"Cannot find book with id {bookUpdate.BookId}");
            }
            book.Description = bookUpdate.Description;

            var entry = dbContext.Entry<BookAggregate>(book);
            entry.State = EntityState.Detached;
            entry.Property(nameof(book.Description)).IsModified = true;


            dbContext.SaveChanges();

            logger.LogInformation("Successfully update {@Book}", entry.Entity);

            return Results.Ok(entry.Entity.Id);
        });
        return routeBuilder;
    }
}
