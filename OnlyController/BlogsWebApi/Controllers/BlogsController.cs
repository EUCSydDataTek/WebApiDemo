﻿using BackendData;
using BackendData.DomainModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogsWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogsController(AppDbContext _context) : ControllerBase
{
    // GET: api/Blogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
    {
        return await _context.Blogs.ToListAsync();
    }

    // GET: api/Blogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Blog>> GetBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);

        if (blog == null)
        {
            return NotFound();
        }

        return blog;
    }

    // POST: api/Blogs
    [HttpPost]
    public async Task<ActionResult<Blog>> PostBlog(Blog blog)
    {
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
    }

    // PUT: api/Blogs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog(int id, Blog blog)
    {
        if (id != blog.BlogId)
        {
            return BadRequest();
        }

        _context.Entry(blog).State = EntityState.Modified;

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
    ///         "value": 0,
    ///         "path": "/rating"
    ///     }
    ///]
    /// </remarks>
    public async Task<ActionResult> PatchBlog(int id, [FromBody] JsonPatchDocument<Blog> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(blog, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(blog).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Blogs/5
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
