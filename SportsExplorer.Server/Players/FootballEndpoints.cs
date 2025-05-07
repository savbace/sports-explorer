using Microsoft.AspNetCore.Mvc;

namespace SportsExplorer.Server.Players;

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