using BlogsWebApi.ApiModels;

namespace BlogsWebApi.Services;

public interface IBlogService
{
    Task<List<BlogDto>> GetAll();
    Task<BlogPostDto?> GetById(int id);
    Task<BlogDto> CreateAndSave(CreateBlogPostDto newBlog);
    Task<BlogDto> UpdateAndSave(UpdateBlogPostDto newBlog);
    Task Delete(int id);

    bool BlogExists(int id);
}
