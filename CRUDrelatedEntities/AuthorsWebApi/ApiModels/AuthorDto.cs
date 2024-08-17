using System.ComponentModel.DataAnnotations;

namespace AuthorsWebApi.ApiModels;

public record AuthorDto
(
    int AuthorId,

    [Required]
    [MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
    string? Name,

    [MaxLength(ConfigConstants.DEFAULT_URI_LENGTH)]
    string? PluralsightUrl,

    [MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
    string? TwitterAlias
);
