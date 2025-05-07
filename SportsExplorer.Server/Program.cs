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

builder.Services.AddScoped<IPlayersProvider, PlayersProvider>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapFallbackToFile("/index.html");

app.AddFootballEndpoints();

app.Run();
