using BackendData.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BackendData;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().HasData(
                new Blog { BlogId = 1, Url = "https://blog1.com", Rating = 2 },
                new Blog { BlogId = 2, Url = "https://blog2.com", Rating = 3 },
                new Blog { BlogId = 3, Url = "https://blog3.com", Rating = 1 },
                new Blog { BlogId = 4, Url = "https://blog5.com", Rating = 3 }
                );

        modelBuilder.Entity<Post>().HasData(
                new Post() { PostId = 1, Title = "Post 1", Content = "Dette er Post 1 i Blog 1", Rating = 2, BlogId = 1 },
                new Post() { PostId = 2, Title = "Post 2", Content = "Dette er Post 2 i Blog 1", Rating = 3, BlogId = 1 },
                new Post() { PostId = 3, Title = "Post 3", Content = "Dette er Post 3 i Blog 1", Rating = 4, BlogId = 1 },
                new Post() { PostId = 4, Title = "Post 4", Content = "Dette er post 4 i Blog 2", Rating = 3, BlogId = 2 },
                new Post() { PostId = 5, Title = "Post 5", Content = "Dette er post 5 i Blog 2", Rating = 1, BlogId = 2 },
                new Post() { PostId = 6, Title = "Post 6", Content = "Dette er post 6 i Blog 3", Rating = 2, BlogId = 3 }
                );

        base.OnModelCreating(modelBuilder);
    }
}
