using Microsoft.AspNetCore.Mvc;
using ModelStateValidation.Models;

namespace ModelStateValidation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpPost]
    public ActionResult<CreateBookInputModel> Post(CreateBookInputModel createBookInputModel)
    {
        //if (createBookInputModel.ISBN!.Length != 10 && createBookInputModel.ISBN.Length != 13)
        //{
        //    ModelState.AddModelError(nameof(createBookInputModel.ISBN), "ISBN should be 10 or 13 numbers long!");
        //}

        //if (!ModelState.IsValid)
        //{
        //    return UnprocessableEntity(ModelState);
        //}

        return Ok(createBookInputModel);
    }
}
