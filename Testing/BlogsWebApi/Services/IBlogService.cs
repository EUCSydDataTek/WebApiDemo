using BlogsWebApi.ApiModels;

namespace BlogsWebApi.Services;

public interface IBlogService
{
    Task<List<BlogDto>> GetAll();
    Task<BlogDto?> GetById(int id);
    Task<BlogDto> CreateAndSave(CreateBlogDto newBlog);
    Task<BlogDto> UpdateAndSave(BlogDto newBlog);
    Task Delete(int id);
    bool BlogExists(int id);
}
