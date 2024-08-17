using BackendData.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BackendData;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();

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

        _ = modelBuilder.Entity<Book>().HasData(
                        new Book
                        {
                            BookId = 1,
                            Title = "Refactoring",
                            Publisher = "Addison-Wesley",
                            AuthorId = 1
                        },
                        new Book
                        {
                            BookId = 2,
                            Title = "Domain-Driven Design",
                            Publisher = "Addison-Wesley",
                            AuthorId = 1
                        },
                        new Book
                        {
                            BookId = 3,
                            Title = "Clean Architecture",
                            Publisher = "Prentice Hall",
                            AuthorId = 2
                        },
                        new Book
                        {
                            BookId = 4,
                            Title = "Implementing Domain-Driven Design",
                            Publisher = "Addison-Wesley",
                            AuthorId = 2
                        }
                    );

        base.OnModelCreating(modelBuilder);
    }
}
