namespace SportsExplorer.Providers.Football;

public interface IFootballProvider
{
    public Task<List<Player>> GetTeams(int seasonId);
    public Task<List<Player>> GetPlayers(int teamId, int seasonId);
}

internal class FootballProvider : IFootballProvider
{
    private readonly IFootballApi api;

    public FootballProvider(IFootballApi api)
    {
        this.api = api;
    }

    public async Task<List<Player>> GetPlayers(int teamId, int seasonId)
    {
        var playerResponse = await api.GetPlayers(teamId, seasonId, new PlayersQuery(seasonId));

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

    public Task<List<Player>> GetTeams(int seasonId)
    {
        throw new NotImplementedException();
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