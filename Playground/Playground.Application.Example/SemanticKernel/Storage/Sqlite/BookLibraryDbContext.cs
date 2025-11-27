using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.Storage.SqlLite;

public class BookLibraryDbContext : DbContext
{

    public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options):
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
