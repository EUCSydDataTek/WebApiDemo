# 1. OnlyController

Dette projekt indeholder et CRUD projekt for Blog-klassen. BankendData-projektet indeholder EntityFramework-filerne, som 
benyttes en SQLite database, der ligger i WebApi-projektet.
I dette første projekt er der kun en controller, som håndterer alle operationer på Blog-klassen.

&nbsp;

## BackendData projektet
BackendData-projektet er et ClassLibrary. 

Diss Nuget-pakkerne er installeret.
- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.EntityFrameworkCore.Tools` 


Gennemgå følgende filer:

- DomainModels/Blog.cs
- AppDbContext.cs (anvender Primary Constructors)

Migrationen udføres med PMB-kommandoen `Add-Migration InitialCreate` og `Update-Database`. 
Husk at vælge BackendData-projektet som Default-projekt i PMB vinduet.

&nbsp;

## BlogsWebApi projektet

| Operation name | URL link          | HTTP method | Input                | Response  | Description             |
|----------------|-------------------|-------------|----------------------|-----------|-------------------------|
| GetBlogs       | /api/blogs        | GET         |                      | List, 200 | Returns all Blogs       |
| GetBlog        | /api/blogs/blogId | GET         | blogId               | Blog, 200 | Returns single Blog     |
| PostBlog       | /api/blogs        | POST        | Blog                 | Blog, 201 | Create af new Blog      |
| PutBlog        | /api/blogs/blogId | PUT         | blogId, Blog         | 204       | Updates a Blog item     |
| PatchBlog      | /api/blogs/blogId | PATCH       | blogId, JsonPatchDoc | 204       | Updates a Blog property |
| DeleteBlog     | /api/blogs        | DELETE      | blogId               | 204       | Deletes a Blog item     |

Gennemgå følgende filer:
- Program.cs (UseSqlite benyttes og EnableSensitiveDataLogging() viser parameter-værdier i loggen). Dependency Injection omtales i næste afsnit af PPT.
- Controllers/BlogsController.cs
    - Gennemgå de forskellige metoder og hvordan de er annoteret med `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpPatch]` og `[HttpDelete]`.
    - Omtal de forskellige checks i metoderne
    - Demo med SwaggerUI og/eller Postman
    - Sammenlign med tabellen oven for.

    &nbsp;

### POST
Følgende JSON kan benyttes til at teste Post-metoden:

**Post document**

	{
        "url": "http://www.google.com",
		"rating": 2	
	}

### PATCH
Følgende JSON kan benyttes til at teste Patch-metoden:

**Patch document**

    [
         {
             "op": "replace",
             "value": 0,
             "path": "/rating"
         }
    ]

