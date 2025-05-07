namespace SportsExplorer.Server.Players;

public interface IPlayersProvider
{
    public Task<List<Player>> GetPlayers(string team, string season);
}

internal class PlayersProvider : IPlayersProvider
{
    private readonly IFootballApi api;

    public PlayersProvider(IFootballApi api)
    {
        this.api = api;
    }

    public async Task<List<Player>> GetPlayers(string team, string season)
    {
        const int seasonId = 719;
        var teamsResponse = await api.GetTeams(new TeamsQuery(seasonId));
        var currentTeam = teamsResponse.Content.FirstOrDefault(t => t.Name.Equals(team, StringComparison.OrdinalIgnoreCase));
        if (currentTeam == null)
        {
            return [];
        }

        var playerResponse = await api.GetPlayers((int)currentTeam.Id, seasonId, new PlayersQuery(seasonId));

        var players = playerResponse.Players
            .Select(p => new Player
            {
                Id = p.Id,
                FirstName = p.Name.First,
                LastName = p.Name.Last,
                Position = p.LatestPosition,
                Birthday = DateTimeOffset.FromUnixTimeMilliseconds(p.Birth.Date.Millis).DateTime,
                ProfilePictureUrl = $"https://resources.premierleague.com/premierleague/photos/players/40x40/{p.AltIds.Opta}.png"
            })
            .ToList();

        return players;
    }
}

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public DateTime Birthday { get; set; }
    public string ProfilePictureUrl { get; set; }
}