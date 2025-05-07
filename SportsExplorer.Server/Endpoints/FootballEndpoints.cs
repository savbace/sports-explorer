using Microsoft.AspNetCore.Mvc;
using SportsExplorer.Providers.Football;

namespace SportsExplorer.Server.Endpoints;

public static class FootballEndpoints
{
    public static void AddFootballEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var seasonEndpoints = endpoints.MapGroup("/api/seasons/{seasonId:int}");

        seasonEndpoints.MapGet("/teams", (int seasonId, [FromServices] IFootballProvider provider) => {
            return provider.GetTeams(seasonId);
        })
        .WithName("GetSeasonTeams");

        seasonEndpoints.MapGet("/teams/{teamId:int}/players", (int seasonId, int teamId, [FromServices] IFootballProvider provider) =>
        {
            return provider.GetPlayers(teamId, seasonId);
        })
        .WithName("GetTeamPlayers");
    }
}