namespace AuthorsWebApi.ApiModels;

public record AuthorBooksDto(
    int AuthorId,
    string Name,
    string? PluralsightUrl,
    string? TwitterAlias,
    List<BookDto> Books);

