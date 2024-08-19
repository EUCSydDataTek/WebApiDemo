using BlogsWebApi.ApiModels;
using BlogsWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogsWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogsController(IBlogService BlogService) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<List<BlogDto>>> GetBlogs()
    {
        List<BlogDto> Blogs = await BlogService.GetAll();

        return Ok(Blogs);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogPostDto>> GetById(int id)
    {
        BlogPostDto? Blog = await BlogService.GetById(id);
        if (Blog == null)
        {
            return NotFound();
        }

        return Ok(Blog);
    }
    #endregion

    #region POST
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<BlogDto>> Create(CreateBlogPostDto newBlog)
    {
        BlogDto BlogDto = await BlogService.CreateAndSave(newBlog);

        return CreatedAtAction("GetById", new { id = BlogDto.BlogId }, BlogDto);
    }
    #endregion

    #region PUT
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogDto>> Update(int id, UpdateBlogPostDto updateBlogPost)
    {
        if (id != updateBlogPost.BlogId)
        {
            return BadRequest();
        }

        if (!BlogService.BlogExists(id))
        {
            return NotFound();
        }
        await BlogService.UpdateAndSave(updateBlogPost);

        return NoContent();
    }
    #endregion

    #region DELETE
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        if (!BlogService.BlogExists(id))
        {
            return NotFound();
        }
        await BlogService.Delete(id);

        return NoContent();
    }
    #endregion
}
