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

public record Player(int Id, string LatestPosition, PlayerName Name, AltIdsInfo AltIds, BirthInfo Birth);

public record PlayerName(string First, string Last);

public record AltIdsInfo(string Opta);

public record BirthInfo(DateInfo Date);

public record DateInfo(long Millis, string Label);

public record PlayerResponse(List<Player> Players);

public record TeamsQuery(
    int CompSeasons,
    int Page = 0,
    int PageSize = 100,
    bool AltIds = false);

public record TeamsResponse(List<Team> Content);

public record Team(double Id, string Name);