using BlogsWebApi.ApiModels;
using BackendData;
using BackendData.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BlogsWebApi.Services;

public class BlogService(AppDbContext context) : IBlogService
{
    public async Task<List<BlogDto>> GetAll()
    {
        List<Blog> Blogs = await context.Blogs.ToListAsync();

        return Blogs
            .Select(a => new BlogDto(a.BlogId, a.Url, a.Rating, a.AccountNumber ))
            .ToList();
    }

    public async Task<BlogDto?> GetById(int id)
    {
        Blog? blog = await context.Blogs.FindAsync(id);

        if (blog == null)
        {
            return null;
        }

        return new BlogDto(blog.BlogId, blog.Url, blog.Rating, blog.AccountNumber);
    }

    public async Task<BlogDto> CreateAndSave(CreateBlogDto newBlog)
    {
        Blog blog = new Blog
        {
            Url = newBlog.Url!,
            Rating = newBlog.Rating,
            AccountNumber = newBlog.AccountNumber!
        };

        await context.Blogs.AddAsync(blog);
        await context.SaveChangesAsync();

        return new BlogDto(blog.BlogId, blog.Url, blog.Rating, blog.AccountNumber);
    }

    public async Task<BlogDto> UpdateAndSave(BlogDto newBlog)
    {
        Blog? blog = await context.Blogs.FindAsync(newBlog.BlogId);
        if (blog == null)
        {
            throw new KeyNotFoundException($"Blog with ID {newBlog.BlogId} not found.");
        }

        // 1. Cannot be used when using DTOs and will update all fields
        // context.Entry(blog).State = EntityState.Modified;   

        // 2. Specify the fields to update
        blog.Url = newBlog.Url!;
        blog.Rating = newBlog.Rating;

        // 3. Or let EF Core do the work and only update the fields that have changed
        //context.Entry(blog!).CurrentValues.SetValues(newBlog);

        await context.SaveChangesAsync();

        return new BlogDto(blog.BlogId, blog.Url, blog.Rating, blog.AccountNumber);
    }

    public async Task Delete(int id)
    {
        //await context.Blogs.Where(a => a.BlogId == id).ExecuteDeleteAsync();

        // Before EF 7.0
        Blog? Blog = await context.Blogs.FindAsync(id);
        context.Remove(Blog!);
        await context.SaveChangesAsync();
    }

    public bool BlogExists(int id)
    {
        return context.Blogs.Any(e => e.BlogId == id);
    }
}
