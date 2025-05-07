namespace SportsExplorer.Server.Players;

public static class FootballEndpoints
{
    public static void AddFootballEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/players", async (string team, IFootballApi api) =>
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
    }
}