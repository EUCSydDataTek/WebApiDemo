using MinimalWebAPIdemo.Models;
using MinimalWebAPIdemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPersonService, PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/hello", () => "Hello, World!");

app.MapGet("/persons/{name}", (string name) => $"Hello, {name}!");

app.MapPost("/persons", (Person person) => $"Hello, {person.Name}!");

app.MapPut("/persons/{name}", (string name, Person person) => $"Hello, {person.Name}!");

app.MapDelete("/persons/{name}", (string name) => $"Goodbye, {name}!");

app.MapGet("/persons", (IPersonService personService) => personService.GetPerson());

app.Run();

