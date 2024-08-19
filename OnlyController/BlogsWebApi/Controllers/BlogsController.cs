using BackendData;
using BackendData.DomainModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogsWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogsController(AppDbContext _context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
    {
        return await _context.Blogs.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Blog>> GetBlog(int id)
    {
        var Blog = await _context.Blogs.FindAsync(id);

        if (Blog == null)
        {
            return NotFound();
        }

        return Blog;
    }

    [HttpPost]
    public async Task<ActionResult<Blog>> PostBlog(Blog Blog)
    {
        _context.Blogs.Add(Blog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBlog", new { id = Blog.BlogId }, Blog);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog(int id, Blog Blog)
    {
        if (id != Blog.BlogId)
        {
            return BadRequest();
        }

        _context.Entry(Blog).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BlogExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    /// <remarks>
    /// [
    ///     {
    ///         "op": "replace",
    ///         "value": "jlerman",
    ///         "path": "/twitterAlias"
    ///     }
    ///]
    /// </remarks>
    public async Task<ActionResult> PatchBlog(int id, [FromBody] JsonPatchDocument<Blog> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var Blog = await _context.Blogs.FindAsync(id);
        if (Blog == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(Blog, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(Blog).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BlogExists(int id)
    {
        return _context.Blogs.Any(e => e.BlogId == id);
    }
}
