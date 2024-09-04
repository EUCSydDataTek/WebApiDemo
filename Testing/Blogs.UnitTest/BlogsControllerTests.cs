using BlogsWebApi.ApiModels;
using BlogsWebApi.Controllers;
using BlogsWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

namespace Blogs.UnitTest;
public class BlogsControllerTests
{
    private readonly Mock<IBlogService> _mockRepo;
    private readonly AccountNumberValidation _validator;
    private readonly BlogsController _controller;

    public BlogsControllerTests()
    {
        _mockRepo = new Mock<IBlogService>();
        _validator = new AccountNumberValidation();
        _controller = new BlogsController(_mockRepo.Object, _validator);
    }

    [Fact]
    public async Task GetBlogs_ActionExecutes_ReturnsOkResultWithBlogs()
    {
        // Arrange
        var expectedBlogs = new List<BlogDto>()
            {
                new BlogDto(1, "https://blog1.com", 2, "123-3452134543-32"),
                new BlogDto(2, "https://blog2.com", 2, "123-9384613085-55")
            };

        _mockRepo.Setup(repo => repo.GetAll())
            .ReturnsAsync(expectedBlogs);

        // Act
        var returnResult = await _controller.GetBlogs();

        // Assert with FluentAssertions
        var okResult = returnResult.Result as OkObjectResult;
        var blogs = okResult!.Value as IEnumerable<BlogDto>;

        returnResult.Result.Should().BeOfType<OkObjectResult>();
        blogs.Should().HaveCount(2);
        blogs.Should().BeEquivalentTo(expectedBlogs);
    }

    
    [Fact]
    public async Task GetById_BlogFound_ReturnsOkResultWithBlog()
    {
        // Arrange
        var expectedBlog = new BlogDto(1, "https://blog1.com", 2, "123-3452134543-32");

        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync(expectedBlog);

        // Act
        var returnResult = await _controller.GetById(1);

        // Assert
        var okResult = returnResult.Result as OkObjectResult;
        var blog = okResult!.Value as BlogDto;

        returnResult.Result.Should().BeOfType<OkObjectResult>();
        blog.Should().BeEquivalentTo(expectedBlog);
    }

    [Fact]
    public async Task GetById_BlogNotFound_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync((BlogDto?)null);

        // Act
        var returnResult = await _controller.GetById(1);

        // Assert
        returnResult.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ValidObjectPassed_ReturnsCreatedResponse()
    {
        // Arrange
        var newBlog = new CreateBlogDto("https://blog3.com", 3, "123-3452134543-32");
        var blogDto = new BlogDto(3, "https://blog3.com", 3, "123-3452134543-32");

        _mockRepo.Setup(repo => repo.CreateAndSave(It.IsAny<CreateBlogDto>()))
            .ReturnsAsync(blogDto);

        // Act
        var returnResult = await _controller.Create(newBlog);

        // Assert
        var createdAtActionResult = returnResult.Result as CreatedAtActionResult;
        var returnValue = createdAtActionResult!.Value as BlogDto;

        returnResult.Result.Should().BeOfType<CreatedAtActionResult>();
        returnValue.Should().BeEquivalentTo(blogDto);
    }

    [Fact]
    public async Task Create_InvalidObjectPassed_ReturnsBadRequest()
    {
        // Arrange
        var newBlog = new CreateBlogDto("https://blog3.com", 3, "invalid-account-number");

        // Act
        var returnResult = await _controller.Create(newBlog);

        // Assert
        returnResult.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Update_ValidObjectPassed_ReturnsNoContent()
    {
        // Arrange
        var blogDto = new BlogDto(3, "https://blog3.com", 3, "123-3452134543-32");

        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync(blogDto);

        _mockRepo.Setup(repo => repo.UpdateAndSave(It.IsAny<BlogDto>()))
            .ReturnsAsync(blogDto);

        // Act
        var returnResult = await _controller.Update(3, blogDto);

        // Assert
        returnResult.Result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Update_InvalidObjectPassed_ReturnsBadRequest()
    {
        // Arrange
        var blogDto = new BlogDto(3, "https://blog3.com", 3, "123-3452134543-32");

        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync(blogDto);

        // Act
        var returnResult = await _controller.Update(3, new BlogDto(3, "https://blog3.com", 3, "invalid-account-number"));

        // Assert
        returnResult.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Update_BlogNotFound_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync((BlogDto?)null);

        // Act
        var returnResult = await _controller.Update(3, new BlogDto(3, "https://blog3.com", 3, "123-3452134543-32"));

        // Assert
        returnResult.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Delete_BlogFound_ReturnsNoContent()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.BlogExists(It.IsAny<int>()))
            .Returns(true);

        // Act
        var returnResult = await _controller.Delete(3);

        // Assert
        returnResult.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_BlogNotFound_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.BlogExists(It.IsAny<int>()))
            .Returns(false);

        // Act
        var returnResult = await _controller.Delete(3);

        // Assert
        returnResult.Should().BeOfType<NotFoundResult>();
    }
}
