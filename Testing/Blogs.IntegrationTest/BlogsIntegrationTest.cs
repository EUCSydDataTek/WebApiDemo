using BlogsWebApi.ApiModels;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Blogs.IntegrationTest;
public class BlogsIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    public BlogsIntegrationTest(TestingWebAppFactory<Program> factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task GetBlogs_ReturnsBlogs()
    {
        // Act
        var response = await _client.GetAsync("/api/Blogs");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        responseString.Should().Contain("https://blog1.com");
        responseString.Should().Contain("https://blog2.com");
    }

    [Fact]
    public async Task GetBlogById_ReturnsBlog()
    {
        // Arrange
        var blog = new CreateBlogDto("https://blog4.com", 2, "214-5874986532-21");

        var response = await _client.PostAsJsonAsync("/api/Blogs", blog);
        BlogDto? createdBlog = await response.Content.ReadFromJsonAsync<BlogDto>();

        // Act
        var result = await _client.GetAsync($"/api/Blogs/{createdBlog!.BlogId}");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        responseString.Should().Contain("https://blog4.com");
    }

    [Fact]
    public async Task GetBlogById_ReturnsWithNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/api/Blogs/1000");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateBlog_ReturnsWithCreatedBlog()
    {
        // Arrange
        var blog = new CreateBlogDto("https://blog4.com", 2, "214-5874986532-21");

        // Act 
        var response = await _client.PostAsJsonAsync("/api/Blogs", blog);
        response.EnsureSuccessStatusCode();
        var createdBlog = await response.Content.ReadFromJsonAsync<BlogDto>();

        // Assert
        createdBlog!.Url.Should().Be("https://blog4.com");
    }

    [Fact]
    public async Task CreateBlog_SentWrongModel_ReturnsBadRequest()
    {
        // Arrange
        var blog = new CreateBlogDto("https://blog4.com", 2, null);

        // Act 
        var response = await _client.PostAsJsonAsync("/api/Blogs", blog);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateBlog_ReturnsWithUpdatedBlog()
    {
        // Arrange
        var blog = new CreateBlogDto("https://blog4.com", 2, "214-5874986532-21");

        var response = await _client.PostAsJsonAsync("/api/Blogs", blog);
        BlogDto? createdBlog = await response.Content.ReadFromJsonAsync<BlogDto>();

        var updatedBlog = new BlogDto(createdBlog!.BlogId, "https://blog4.com", 3, "214-5874986532-21");

        // Act 
        response = await _client.PutAsJsonAsync($"/api/Blogs/{createdBlog!.BlogId}", updatedBlog);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Act
        var result = await _client.GetAsync($"/api/Blogs/{createdBlog!.BlogId}");
        response.EnsureSuccessStatusCode();
        BlogDto? myBlog = await result.Content.ReadFromJsonAsync<BlogDto>();

        // Assert
        myBlog!.Rating.Should().Be(3);
    }

    [Fact]
    public async Task UpdateBlog_ReturnsWithBadRequest()
    {
        // Arrange
        var blog = new CreateBlogDto("https://blog4.com", 2, "214-5874986532-21");

        var response = await _client.PostAsJsonAsync("/api/Blogs", blog);
        BlogDto? createdBlog = await response.Content.ReadFromJsonAsync<BlogDto>();

        var updatedBlog = new BlogDto(createdBlog!.BlogId, "https://blog4.com", 3, null);

        // Act 
        response = await _client.PutAsJsonAsync($"/api/Blogs/{createdBlog!.BlogId}", updatedBlog);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteBlog_ReturnsWithNoContent()
    {
        // Arrange
        var blog = new CreateBlogDto("https://blog4.com", 2, "214-5874986532-21");

        var response = await _client.PostAsJsonAsync("/api/Blogs", blog);
        BlogDto? createdBlog = await response.Content.ReadFromJsonAsync<BlogDto>();

        // Act 
        response = await _client.DeleteAsync($"/api/Blogs/{createdBlog!.BlogId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteBlog_ReturnsWithNotFound()
    {
        // Act 
        var response = await _client.DeleteAsync($"/api/Blogs/1000");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
