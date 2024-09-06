# 2. ReturnTypesDemo

Dette projekt viser 3 forskellige måder at returnere data fra en controller i en ASP.NET Core Web API. Studér SwaggerUI
for at se forskellen på de 3 metoder.

Demonstres vha. SwaggerUI

### En GET, der returner en specifik typen, nemlig et Blog-objekt

	public async Task<Blog> GetBlogSpecificType(int id)

De forskellige metoder, så som `Ok()` og `NotFound()` fungerer ikke med specikke response-typer.

Men SwaggerUI viser, at det er en Blog, der returneres.

&nbsp;

### En GET, der returnerer `IActionResult`

	public async Task<IActionResult> GetBlogIActionResult(int id)

Metoderne Ok() og NotFound() fungerer med IActionResult.

Men SwaggerUI giver ingen information om, hvad der returneres.

&nbsp;

### En GET, der returnerer `ActionResult<Blog>`

	public async Task<ActionResult<Blog>> GetBlogActionResult(int id)

Metoderne Ok() og NotFound() fungerer med ActionResult<Blog>.

SwaggerUI viser, at det er en Blog, der returneres.

&nbsp;

### POST, der returnerer `IActionResult`  

	 [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Blog))]   // Type is needed
	 public async Task<IActionResult> BlogBlogIActionResult(Blog Blog)

Her er det nødvendigt at angive, hvilken type der returneres, da IActionResult ikke giver denne information.

&nbsp;

### POST, der returnerer `ActionResult<Blog>`

	[ProducesResponseType(StatusCodes.Status201Created)]   // Type is not needed
	public async Task<ActionResult<Blog>> BlogBlogActionResult(Blog Blog)

&nbsp;

### Konklusion

Benyt `ActionResult<T>` i stedet for `IActionResult`, da det giver mere information i SwaggerUI.