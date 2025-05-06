using Refit;

namespace SportsExplorer.Server.Players;

public record PlayersQueryParams(
    int CompSeason,
    int Page = 0,
    int PageSize = 10,
    bool AltIds = true,
    string Type = "player");

public record Player(int Id, string LatestPosition);

public record PlayerResponse(List<Player> Players);

public interface IFootballApi
{
    [Get("/football/teams/{teamId}/compseasons/{seasonId}/staff")]
    Task<PlayerResponse> GetPlayers(int teamId, int seasonId, PlayersQueryParams query);
}
