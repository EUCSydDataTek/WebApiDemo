namespace BlogsWebApi.ApiModels;

public record BlogPostDto(
    int BlogId,
    string Url,
    int? Rating,
    List<PostDto> Posts);

