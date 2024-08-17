using BackendData;
using BackendData.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorsWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorsController(AppDbContext _context) : ControllerBase
{
    #region GET
    [HttpGet("SpecificType/{id}")]
    public async Task<Author> GetAuthorSpecificType(int id)
    {
        Author? author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            //return NotFound();    // NotFound() cannot be used with specifik type
        }

        //return Ok(author!);       // Ok() cannot be used with specifik type
        return author!;
    }

    [HttpGet("IActionResult/{id}")]
    public async Task<IActionResult> GetAuthorIActionResult(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpGet("ActionResult/{id}")]
    public async Task<ActionResult<Author>> GetAuthorActionResult(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return await _context.Authors.ToListAsync();
    }
    #endregion

    #region POST
    [HttpPost("IActionResult")]
    [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(Author))]   // Type is needed
    public async Task<IActionResult> PostAuthorIActionResult(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
    }

    [HttpPost("Actionresult")]
    [ProducesResponseType(StatusCodes.Status201Created)]    // Type is not needed because it is already defined in the ActionResult<Author>
    public async Task<ActionResult<Author>> PostAuthorActionResult(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
    }

    #endregion
}
