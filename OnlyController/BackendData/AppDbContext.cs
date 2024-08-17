using BackendData.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BackendData;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Author>().HasData(
                        new Author
                        {
                            AuthorId = 1,
                            Name = "Martin Fowler",
                            PluralsightUrl = "https://app.pluralsight.com/profile/martin-fowler",
                            TwitterAlias = "https://twitter.com/martinfawler"
                        },
                        new Author
                        {
                            AuthorId = 2,
                            Name = "Eric Evans",
                            PluralsightUrl = "https://app.pluralsight.com/profile/eric-evans",
                            TwitterAlias = "https://twitter.com/ericevans"
                        },
                        new Author
                        {
                            AuthorId = 3,
                            Name = "Steve Smith",
                            PluralsightUrl = "https://app.pluralsight.com/profile/steve-smith",
                            TwitterAlias = "https://twitter.com/stevesmith"
                        }
                    );

        base.OnModelCreating(modelBuilder);
    }
}
