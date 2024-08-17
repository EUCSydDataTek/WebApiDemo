namespace BackendData.DomainModels;
public class Book
{
    public int BookId { get; set; }
    public string? Title { get; set; }
    public string? Publisher { get; set; }
    public int AuthorId { get; set; }   // Foreign Key
    public Author Author { get; set; }  // Reference Navigation Property
}
