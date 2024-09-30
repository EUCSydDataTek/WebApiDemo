using BackendData;
using BackendData.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogsWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogsController(AppDbContext _context) : ControllerBase
{
    #region GET
    [HttpGet("SpecificType/{id}")]
    public async Task<Blog> GetBlogSpecificType(int id)
    {
        Blog? Blog = await _context.Blogs.FindAsync(id);

        if (Blog == null)
        {
            //return NotFound();    // NotFound() cannot be used with specifik type
        }

        //return Ok(Blog!);       // Ok() cannot be used with specifik type
        return Blog!;
    }

    [HttpGet("IActionResult/{id}")]
    public async Task<IActionResult> GetBlogIActionResult(int id)
    {
        var Blog = await _context.Blogs.FindAsync(id);

        if (Blog == null)
        {
            return NotFound();
        }

        return Ok(Blog);
    }

    [HttpGet("ActionResult/{id}")]
    public async Task<ActionResult<Blog>> GetBlogActionResult(int id)
    {
        var Blog = await _context.Blogs.FindAsync(id);

        if (Blog == null)
        {
            return NotFound();
        }

        return Ok(Blog);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
    {
        return await _context.Blogs.ToListAsync();
    }
    #endregion

    #region POST
    [HttpPost("IActionResult")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Blog))]   // Type is needed
    public async Task<IActionResult> BlogBlogIActionResult(Blog Blog)
    {
        _context.Blogs.Add(Blog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBlogActionResult", new { id = Blog.BlogId }, Blog);
    }

    [HttpPost("Actionresult")]
    [ProducesResponseType(StatusCodes.Status201Created)]    // Type is not needed because it is already defined in the ActionResult<Blog>
    public async Task<ActionResult<Blog>> BlogBlogActionResult(Blog Blog)
    {
        _context.Blogs.Add(Blog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBlogActionResult", new { id = Blog.BlogId }, Blog);
    }

    #endregion
}
