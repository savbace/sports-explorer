using Refit;
using SportsExplorer.Server.Players;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddRefitClient<IFootballApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://footballapi.pulselive.com"));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/api/players", async (IFootballApi api) => {
    var playerResponse = await api.GetPlayers(1, 719, new PlayersQueryParams(719));

    return playerResponse.Players;
})
.WithName("GetPlayers");

app.MapFallbackToFile("/index.html");

app.Run();
