using StarLs.Core.Entities;

namespace StarLs.Application.Queries.Movies;

public class GetMovieQueryResponse
{
    public short Id { get; set; }
    public string Title { get; private set; } = null!;
    public short Episode { get; private set; }
    public string OpeningCrawl { get; private set; } = null!;
    public string Director { get; private set; } = null!;
    public string Producer { get; private set; } = null!;
    public string ReleaseDate { get; private set; } = null!;
    public List<Character> Characters { get; private set; } = null!;
    public List<Planet> Planets { get; private set; } = null!;
    public List<Vehicle> Vehicles { get; private set; } = null!;
    public List<Starship> Starships { get; private set; } = null!;
}
