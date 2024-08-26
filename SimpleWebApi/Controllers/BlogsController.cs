using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Models;

namespace SimpleWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Blog>> GetBlogs()
    {
        return new List<Blog>
        {
            new Blog { BlogId = 1, Url = "https://blog1.com", Rating = 2 },
            new Blog { BlogId = 2, Url = "https://blog2.com", Rating = 3 },
            new Blog { BlogId = 3, Url = "https://blog3.com", Rating = 1 },
            new Blog { BlogId = 4, Url = "https://blog5.com", Rating = 3 }
        };
    }
}