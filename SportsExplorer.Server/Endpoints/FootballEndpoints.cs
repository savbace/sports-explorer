using Microsoft.AspNetCore.Mvc;
using SportsExplorer.Providers.Football;

namespace SportsExplorer.Server.Endpoints;

public static class FootballEndpoints
{
    public static void AddFootballEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/teams/{teamId:int}/seasons/{seasonId:int}/players", (int teamId, int seasonId, [FromServices] IFootballProvider provider) =>
        {
            return provider.GetPlayers(teamId, seasonId);
        })
        .WithName("GetTeamPlayers");
    }
}