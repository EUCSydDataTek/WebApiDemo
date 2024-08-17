using AuthorsWebApi.ApiModels;
using AuthorsWebApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AuthorsWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<List<AuthorDto>>> Index(CancellationToken cancellationToken)
    {
        List<AuthorDto> authors = await authorService.GetAll();

        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorBooksDto>> GetById(int id)
    {
        AuthorBooksDto? author = await authorService.GetById(id);
        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }
    #endregion

    #region POST
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<AuthorDto>> Create(CreateAuthorBookDto newAuthor)
    {
        AuthorDto authorDto = await authorService.CreateAndSave(newAuthor);

        return Created($"authors/{authorDto.AuthorId}", authorDto);
    }
    #endregion

    #region PUT
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorDto>> Update(int id, AuthorDto editAuthor)
    {
        if (id != editAuthor.AuthorId)
        {
            return BadRequest();
        }

        if (!authorService.AuthorExists(id))
        {
            return NotFound();
        }
        await authorService.UpdateAndSave(editAuthor);

        return NoContent();
    }
    #endregion

    //#region PATCH
    //[HttpPatch("{id}")]
    ///// <remarks>
    ///// [
    /////     {
    /////         "op": "replace",
    /////         "value": "jlerman",
    /////         "path": "/twitterAlias"
    /////     }
    /////]
    ///// </remarks>
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AuthorDto> patchDoc)
    //{
    //    if (patchDoc == null)
    //    {
    //        return BadRequest();
    //    }

    //    AuthorDto? authorDto = await authorService.GetById(id);
    //    if (authorDto == null)
    //    {
    //        return NotFound();
    //    }

    //    // Create a mutable copy of the record
    //    AuthorDto? authorDtoCopy = authorDto with { };

    //    patchDoc.ApplyTo(authorDtoCopy, ModelState);

    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    await authorService.UpdateAndSave(authorDtoCopy);

    //    return NoContent();
    //}
    //#endregion

    #region DELETE
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        if (!authorService.AuthorExists(id))
        {
            return NotFound();
        }
        await authorService.Delete(id);

        return NoContent();
    }
    #endregion
}
