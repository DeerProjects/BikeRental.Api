using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// --- Add services ---
builder.Services.AddOpenApi();               // .NET 8 built-in OpenAPI spec
builder.Services.AddEndpointsApiExplorer();  // Needed for Swagger UI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bicycle Rent API",
        Version = "v1",
        Description = "Sample .NET 8 Minimal API demonstrating Swagger + OpenAPI integration."
    });
});

var app = builder.Build();

// --- Configure pipeline ---
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Forecast API v1");
        c.RoutePrefix = "swagger";  
    });
}

// ✅ Explicitly define ports (helps HTTPS redirect logic)
app.Urls.Add("https://localhost:7083");
app.Urls.Add("http://localhost:5131");

app.UseHttpsRedirection(); // ✅ correct form for .NET 8

// --- Endpoint: Weather Forecast ---
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/Bicycle Rent", (int days = 5) =>
{
    if (days is < 1 or > 14)
        return Results.BadRequest(new ApiError("VALIDATION", "Days must be between 1 and 14"));

    var forecast = Enumerable.Range(1, days).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();

    return Results.Ok(forecast);
})
.WithName("GetWeatherForecast")
.WithTags("Weather")
.WithOpenApi(op =>
{
    op.Summary = "Get daily weather forecast";
    op.Description = "Returns a random forecast for the next N days.";
    op.Parameters[0].Description = "Number of forecast days (1–14, default 5)";
    op.Responses["200"].Description = "List of weather forecasts";
    op.Responses["400"].Description = "Invalid days parameter";
    return op;
})
.Produces<IEnumerable<WeatherForecast>>(StatusCodes.Status200OK)
.Produces<ApiError>(StatusCodes.Status400BadRequest);

app.Run();

// --- Data models ---
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record ApiError(string Code, string Message);
