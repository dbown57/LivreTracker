using Core.Features.Books;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class LivreTrackerContext(DbContextOptions<LivreTrackerContext> options) : DbContext(options)
{
    public DbSet<BookItem> TodoItems { get; set; } = null!;
}