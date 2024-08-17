using AuthorsWebApi.ApiModels;
using BackendData;
using BackendData.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AuthorsWebApi.Services;

public class AuthorService(AppDbContext context) : IAuthorService
{
    public async Task<List<AuthorDto>> GetAll()
    {
        List<Author> authors = await context.Authors.ToListAsync();

        return authors
            .Select(a => new AuthorDto(a.AuthorId, a.Name, a.PluralsightUrl, a.TwitterAlias ?? "" ))
            .ToList();
    }

    public async Task<AuthorBooksDto?> GetById(int id)
    {
        // Eagerly load the author along with their books
        Author? author = await context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.AuthorId == id);

        if (author == null)
        {
            return null;
        }

        return new AuthorBooksDto(
            author.AuthorId,
            author.Name,
            author.PluralsightUrl,
            author.TwitterAlias ?? "",
            author.Books.Select(b => new BookDto(b.BookId, b.Title!, b.Publisher!)).ToList()
        );
    }

    public async Task<AuthorDto> CreateAndSave(CreateAuthorBookDto newAuthor)
    {
        // Map the CreateAuthorDto to an Author object
        Author author = new Author
        {
            Name = newAuthor.Name!,
            PluralsightUrl = newAuthor.PluralsightUrl,
            TwitterAlias = newAuthor.TwitterAlias,
            Books = newAuthor.Books?.Select(b => new Book
            {
                Title = b.Title,
                Publisher = b.Publisher,
            }).ToList() ?? new List<Book>()
        };

        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();

        return new AuthorDto(author.AuthorId, author.Name, author.PluralsightUrl, author.TwitterAlias ?? "");
    }

    public async Task<AuthorDto> UpdateAndSave(AuthorDto newAuthor)
    {
        Author? author = await context.Authors.FindAsync(newAuthor.AuthorId);
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with ID {newAuthor.AuthorId} not found.");
        }

        // 1. Cannot be used when using DTOs and will update all fields
        // context.Entry(author).State = EntityState.Modified;   

        // 2. Specify the fields to update
        //author.Name = newAuthor.Name!;
        //author.PluralsightUrl = newAuthor.PluralsightUrl;
        //author.TwitterAlias = newAuthor.TwitterAlias;

        // 3. Or let EF Core do the work and only update the fields that have changed
        context.Entry(author!).CurrentValues.SetValues(newAuthor);

        await context.SaveChangesAsync();

        return new AuthorDto(author.AuthorId, author.Name, author.PluralsightUrl, author.TwitterAlias ?? "");
    }

    public async Task Delete(int id)
    {
        await context.Authors.Where(a => a.AuthorId == id).ExecuteDeleteAsync();

        // Before EF 7.0
        //Author? author = await context.Authors.FindAsync(id);
        //context.Remove(author!);
        //await context.SaveChangesAsync();
    }

    public bool AuthorExists(int id)
    {
        return context.Authors.Any(e => e.AuthorId == id);
    }
}
