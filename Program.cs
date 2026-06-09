using System;
using System.Threading.Tasks;
using TmsCore.Exercises; // Kept from your old setup just in case

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// 🚀 REGISTER YOUR SERVICE RIGHT HERE
builder.Services.AddSingleton<IEnrollmentService, EnrollmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Optional: If you want an endpoint to trigger your old exercises for testing, 
// you can map them to a route! Otherwise, you can safely comment these out.
app.MapGet("/run-exercises", async () => {
    await Exercise6Async.Run();
    Exercise7Exceptions.Run();
    return Results.Ok("Exercises executed in console output.");
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}