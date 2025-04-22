using Microsoft.EntityFrameworkCore;

public class LivreTrackerContext(DbContextOptions<LivreTrackerContext> options) : DbContext(options)
{
    public DbSet<BookItem> BookItems { get; set; } = null!;
}