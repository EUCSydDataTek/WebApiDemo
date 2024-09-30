using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    #region Routing Examples
    [HttpGet]
    public ActionResult<List<Post>> GetPosts()
    {
        List<Post> posts = new List<Post> 
        {
            new Post(1, "First Post", "This is the first post", 5, 1),
            new Post(2, "Second Post", "This is the second post", 4, 2),
            new Post(3, "Third Post", "This is the third post", 3, 3)
        };
        return Ok(posts);
    }

    [HttpGet("{id}")]
    //[HttpGet("{id:int}")]
    public ActionResult<Post> GetPost(int id)
    {
        return Ok(new Post(id, "First Post", "This is the first post", 5, 3));
    }

    [HttpPut("{id}/publish")]
    public ActionResult PublishPost(int id, Post post)
    {
        return Ok($"Post published successfully with id = {id}");
    }

    //[HttpGet("{userId}")]
    //public ActionResult<Post> GetPostByUserId_AmbiguousMatchException(int userId)
    //{
    //    return Ok(new Post(1, "First Post", "This is the first post", 5, 3));
    //}
    #endregion

    #region Route Parameter Example
    [HttpGet("user/{userId}")] 
    public ActionResult<Post> GetPostByRouteUserId(int userId)
    {
        return Ok(new Post(1, "Route Parameter", $"This is a post by {userId}", 5, userId));
    }

    [HttpGet("user")]
    public ActionResult<Post> GetPostByQueryUserId(int userId)
    {
        return Ok(new Post(1, "QueryString Parameter", $"This is a post by {userId}", 5, userId));
    }

    [HttpPost]
    public ActionResult CreatePost(Post post)
    {
        Post newPost = post with { PostId = 42 };
        return CreatedAtAction(nameof(GetPost), new { id = newPost.PostId }, newPost);
    }
    #endregion
}
