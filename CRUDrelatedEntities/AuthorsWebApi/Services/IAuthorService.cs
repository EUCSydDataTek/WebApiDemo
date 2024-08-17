using AuthorsWebApi.ApiModels;

namespace AuthorsWebApi.Services;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAll();
    Task<AuthorBooksDto?> GetById(int id);
    Task<AuthorDto> CreateAndSave(CreateAuthorBookDto newAuthor);
    Task<AuthorDto> UpdateAndSave(AuthorDto newAuthor);
    Task Delete(int id);

    bool AuthorExists(int id);
}
