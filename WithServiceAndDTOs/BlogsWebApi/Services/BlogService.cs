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
            .Select(a => new BlogDto(a.BlogId, a.Url, a.Rating ))
            .ToList();
    }

    public async Task<BlogDto?> GetById(int id)
    {
        Blog? Blog = await context.Blogs.FindAsync(id);

        if (Blog == null)
        {
            return null;
        }

        return new BlogDto(Blog.BlogId, Blog.Url, Blog.Rating);
    }

    public async Task<BlogDto> CreateAndSave(CreateBlogDto newBlog)
    {
        Blog Blog = new Blog
        {
            Url = newBlog.Url!,
            Rating = newBlog.Rating
        };

        await context.Blogs.AddAsync(Blog);
        await context.SaveChangesAsync();

        return new BlogDto(Blog.BlogId, Blog.Url, Blog.Rating);
    }

    public async Task<BlogDto> UpdateAndSave(BlogDto newBlog)
    {
        Blog? Blog = await context.Blogs.FindAsync(newBlog.BlogId);
        if (Blog == null)
        {
            throw new KeyNotFoundException($"Blog with ID {newBlog.BlogId} not found.");
        }

        // 1. Cannot be used when using DTOs and will update all fields
        // context.Entry(Blog).State = EntityState.Modified;   

        // 2. Specify the fields to update
        Blog.Url = newBlog.Url!;
        Blog.Rating = newBlog.Rating;

        // 3. Or let EF Core do the work and only update the fields that have changed
        //context.Entry(Blog!).CurrentValues.SetValues(newBlog);

        await context.SaveChangesAsync();

        return new BlogDto(Blog.BlogId, Blog.Url, Blog.Rating);
    }

    public async Task Delete(int id)
    {
        await context.Blogs.Where(a => a.BlogId == id).ExecuteDeleteAsync();

        // Before EF 7.0
        //Blog? Blog = await context.Blogs.FindAsync(id);
        //context.Remove(Blog!);
        //await context.SaveChangesAsync();
    }

    public bool BlogExists(int id)
    {
        return context.Blogs.Any(e => e.BlogId == id);
    }
}
