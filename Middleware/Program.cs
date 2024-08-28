using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRateLimiter(_ =>
    _.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello world!");
//});

//app.Use(async (context, next) =>
//{
//    var logger = app.Services.GetRequiredService<ILogger<Program>>();
//    logger.LogInformation($"Request Host: {context.Request.Host}");
//    logger.LogInformation("My Middleware - Before");
//    await next(context);
//    logger.LogInformation("My Middleware - After");
//    logger.LogInformation($"Response StatusCode: {context.Response.
//    StatusCode}");
//});


app.UseRateLimiter();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
