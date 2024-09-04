using BackendData.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BackendData;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().HasData(
                new Blog { BlogId = 1, Url = "https://blog1.com", Rating = 2, AccountNumber = "123-3452134543-32" },
                new Blog { BlogId = 2, Url = "https://blog2.com", Rating = 3, AccountNumber = "123-9384613085-55" },
                new Blog { BlogId = 3, Url = "https://blog3.com", Rating = 1, AccountNumber = "117-5384643035-94" },
                new Blog { BlogId = 4, Url = "https://blog5.com", Rating = 3, AccountNumber = "118-1354643035-77" }
                );

        base.OnModelCreating(modelBuilder);
    }
}
