using Refit;

namespace SportsExplorer.Server.Players;

public interface IFootballApi
{
    [Get("/football/teams/{teamId}/compseasons/{seasonId}/staff")]
    Task<PlayerResponse> GetPlayers(int teamId, int seasonId, PlayersQuery query);

    [Get("/football/teams")]
    Task<TeamsResponse> GetTeams(TeamsQuery query);
}

public record PlayersQuery(
    int CompSeasons,
    int Page = 0,
    int PageSize = 10,
    bool AltIds = true,
    string Type = "player");

public record Player(int Id, string LatestPosition);

public record PlayerResponse(List<Player> Players);

public record TeamsQuery(
    int CompSeasons,
    int Page = 0,
    int PageSize = 100,
    bool AltIds = false);

public record TeamsResponse(List<Team> Content);

public record Team(double Id, string Name);