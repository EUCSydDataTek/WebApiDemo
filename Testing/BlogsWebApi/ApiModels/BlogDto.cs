using System.ComponentModel.DataAnnotations;

namespace BlogsWebApi.ApiModels;

public record BlogDto
(
    int BlogId,

    [Required]
    [MaxLength(ConfigConstants.DEFAULT_URL_LENGTH)]
    string? Url,

    [Range(ConfigConstants.DEFAULT_RANGE_MIN, ConfigConstants.DEFAULT_RANGE_MAX)]
    int? Rating,

    [Required(ErrorMessage = "Account number is required")]
    string? AccountNumber
);

//public class BlogDto
//{
//    public int BlogId { get; set; }

//    [Required]
//    [MaxLength(50)]
//    public string? Url { get; set; }

//    [Range(1, 3)]
//    public int? Rating { get; set; }

//    [Required(ErrorMessage = "Account number is required")]
//    public string? AccountNumber { get; set; }
//}
