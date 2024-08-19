# 3. WithServiceAndDTOs

Her er oprettet en Blog-service med følgende interfaces:

    public interface IBlogService
    {
        Task<List<BlogDto>> GetAll();
        Task<BlogDto?> GetById(int id);
        Task<BlogDto> CreateAndSave(CreateBlogDto newBlog);
        Task<BlogDto> UpdateAndSave(BlogDto newBlog);
        Task Delete(int id);
        bool BlogExists(int id);
    }

Servicen er implementeret i klassen `BlogService` og er registreret i `Program`-klassen:

	builder.Services.AddScoped<IBlogService, BlogService>();

&nbsp;

## DTOs

Der er oprettet følgende DTOs med records:

    public record BlogDto
    (
        int BlogId,

        [Required]
        [MaxLength(ConfigConstants.DEFAULT_URL_LENGTH)]
        string? Url,

        [Range(ConfigConstants.DEFAULT_RANGE_MIN, ConfigConstants.DEFAULT_RANGE_MAX)]
        int? Rating
    );

    public record CreateBlogDto
    (
        [Required]
        [MaxLength(ConfigConstants.DEFAULT_URL_LENGTH)]
        string? Url,

        [Range(ConfigConstants.DEFAULT_RANGE_MIN, ConfigConstants.DEFAULT_RANGE_MAX)]
        int? Rating
    );

&nbsp;

