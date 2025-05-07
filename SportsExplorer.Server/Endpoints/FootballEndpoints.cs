using Microsoft.AspNetCore.Mvc;
using SportsExplorer.Providers.Players;

namespace SportsExplorer.Server.Endpoints;

public static class FootballEndpoints
{
    public static void AddFootballEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/players", (string team, [FromServices] IPlayersProvider provider) =>
        {
            return provider.GetPlayers(team, "TODO");
        })
        .WithName("GetPlayers");
    }
}