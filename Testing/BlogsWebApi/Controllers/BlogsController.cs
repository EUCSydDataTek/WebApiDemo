﻿using BlogsWebApi.ApiModels;
using BlogsWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogsWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController(IBlogService BlogService, AccountNumberValidation validation) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<List<BlogDto>>> GetBlogs()
    {
        List<BlogDto> blogs = await BlogService.GetAll();

        return Ok(blogs);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogDto>> GetById(int id)
    {
        BlogDto? blog = await BlogService.GetById(id);
        if (blog == null)
        {
            return NotFound();
        }

        return Ok(blog);
    }
    #endregion

    #region POST
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<BlogDto>> Create(CreateBlogDto newBlog)
    {
        if (!validation.IsValid(newBlog.AccountNumber!))
        {
            ModelState.AddModelError("AccountNumber", "Account Number is invalid");
            return BadRequest(ModelState);
        }

        BlogDto blogDto = await BlogService.CreateAndSave(newBlog);

        return CreatedAtAction("GetById", new { id = blogDto.BlogId }, blogDto);
    }
    #endregion

    #region PUT
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogDto>> Update(int id, BlogDto editBlog)
    {
        if (id != editBlog.BlogId)
        {
            return BadRequest();
        }
        if (!validation.IsValid(editBlog.AccountNumber!))
        {
            ModelState.AddModelError("AccountNumber", "Account Number is invalid");
            return BadRequest(ModelState);
        }

        BlogDto? blogDto = await BlogService.GetById(id);
        if (blogDto == null)
        {
            return NotFound();
        }
        blogDto = await BlogService.UpdateAndSave(editBlog);

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