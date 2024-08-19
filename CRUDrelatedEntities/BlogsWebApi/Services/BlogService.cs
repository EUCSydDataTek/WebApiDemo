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
            .Select(a => new BlogDto(a.BlogId, a.Url, a.Rating))
            .ToList();
    }

    public async Task<BlogPostDto?> GetById(int id)
    {
        // Eagerly load the Blog along with their books
        Blog? Blog = await context.Blogs
            .Include(a => a.Posts)
            .FirstOrDefaultAsync(a => a.BlogId == id);

        if (Blog == null)
        {
            return null;
        }

        return new BlogPostDto(
            Blog.BlogId,
            Blog.Url!,
            Blog.Rating,
            Blog.Posts.Select(b => new PostDto(b.PostId, b.Title!, b.Content!, b.Rating)).ToList()
        );
    }

    public async Task<BlogDto> CreateAndSave(CreateBlogPostDto newBlog)
    {
        // Map the CreateBlogDto to an Blog object
        Blog Blog = new Blog
        {
            Url = newBlog.Url!,
            Rating = newBlog.Rating,
            Posts = newBlog.Posts?.Select(b => new Post
            {
                Title = b.Title,
                Content = b.Content,
                Rating = b.Rating
            }).ToList() ?? new List<Post>()
        };

        await context.Blogs.AddAsync(Blog);
        await context.SaveChangesAsync();

        return new BlogDto(Blog.BlogId, Blog.Url, Blog.Rating);
    }

    public async Task<BlogDto> UpdateAndSave(UpdateBlogPostDto newBlog)
    {
        // Eagerly load the Blog along with their posts
        Blog? existingBlog = await context.Blogs
            .Include(b => b.Posts)
            .FirstOrDefaultAsync(b => b.BlogId == newBlog.BlogId);

        if (existingBlog == null)
        {
            throw new KeyNotFoundException($"Blog with ID {newBlog.BlogId} not found.");
        }

        // Update existing Blog
        context.Entry(existingBlog!).CurrentValues.SetValues(newBlog);

        // Update the related Posts
        foreach (var newPost in newBlog.Posts!)
        {
            var existingPost = existingBlog.Posts
                .FirstOrDefault(p => p.PostId == newPost.PostId);

            if (existingPost != null)
            {
                // Update existing post
                context.Entry(existingPost).CurrentValues.SetValues(newPost);
            }
            else
            {
                // Add new post
                existingBlog.Posts.Add(new Post
                {
                    Title = newPost.Title,
                    Content = newPost.Content,
                    Rating = newPost.Rating
                });
            }
        }

        await context.SaveChangesAsync();

        return new BlogDto(existingBlog.BlogId, existingBlog.Url, existingBlog.Rating);
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
