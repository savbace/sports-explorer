using Refit;

namespace SportsExplorer.Providers;

internal interface IFootballApi
{
    [Get("/football/teams/{teamId}/compseasons/{seasonId}/staff")]
    Task<PlayerResponse> GetPlayers(int teamId, int seasonId, PlayersQuery query);

    [Get("/football/teams")]
    Task<TeamsResponse> GetTeams(TeamsQuery query);
}

internal record PlayersQuery(
    int CompSeasons,
    int Page = 0,
    int PageSize = 10,
    bool AltIds = true,
    string Type = "player");

internal record PlayerInfo(int Id, string LatestPosition, PlayerName Name, AltIdsInfo AltIds, BirthInfo Birth);

internal record PlayerName(string First, string Last);

internal record AltIdsInfo(string Opta);

internal record BirthInfo(DateInfo Date);

internal record DateInfo(long Millis, string Label);

internal record PlayerResponse(List<PlayerInfo> Players);

internal record TeamsQuery(
    int CompSeasons,
    int Page = 0,
    int PageSize = 100,
    bool AltIds = false);

internal record TeamsResponse(List<TeamInfo> Content);

internal record TeamInfo(double Id, string Name);