using System.Text.Json;
using Refit;
using SportsExplorer.Server.Players;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.ConfigureHttpJsonOptions(options =>
// {
//     options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
// });

builder.Services
    .AddRefitClient<IFootballApi>(
        new RefitSettings(
            new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)))
        {
            UrlParameterKeyFormatter = new CamelCaseUrlParameterKeyFormatter()
        })
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


app.MapGet("/api/players", async (string team, IFootballApi api) =>
{
    const int seasonId = 719;
    var teamsResponse = await api.GetTeams(new TeamsQuery(seasonId));
    var currentTeam = teamsResponse.Content.FirstOrDefault(t => t.Name.Equals(team, StringComparison.OrdinalIgnoreCase));
    if (currentTeam == null)
    {
        return [];
    }

    var playerResponse = await api.GetPlayers((int)currentTeam.Id, seasonId, new PlayersQuery(seasonId));

    return playerResponse.Players;
})
.WithName("GetPlayers");

app.MapFallbackToFile("/index.html");

app.Run();
