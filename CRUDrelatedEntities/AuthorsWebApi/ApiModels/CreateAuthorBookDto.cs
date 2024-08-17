using System.ComponentModel.DataAnnotations;

namespace AuthorsWebApi.ApiModels;

public record CreateAuthorBookDto
    (
        [Required]
        [MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
        string? Name,

        [MaxLength(ConfigConstants.DEFAULT_URI_LENGTH)]
        string? PluralsightUrl,

        [MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
        string? TwitterAlias,

        List<BookDto>? Books = null
    );
