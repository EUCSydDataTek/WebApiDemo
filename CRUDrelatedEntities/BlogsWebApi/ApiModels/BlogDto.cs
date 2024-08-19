using System.ComponentModel.DataAnnotations;

namespace BlogsWebApi.ApiModels;

public record BlogDto
(
    int BlogId,

    [Required]
    [MaxLength(ConfigConstants.DEFAULT_URL_LENGTH)]
    string? Url,

    [Range(ConfigConstants.DEFAULT_RANGE_MIN, ConfigConstants.DEFAULT_RANGE_MAX)]
    int? Rating
);
