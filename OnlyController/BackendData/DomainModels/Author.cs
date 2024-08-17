namespace BackendData.DomainModels;
public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = "New Author";
    public string? PluralsightUrl { get; set; }
    public string? TwitterAlias { get; set; }
}
