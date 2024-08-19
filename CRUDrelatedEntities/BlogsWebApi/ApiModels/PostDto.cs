namespace BlogsWebApi.ApiModels;

public record PostDto(
    int PostId,
    string? Title,
    string? Content,
    int Rating);
