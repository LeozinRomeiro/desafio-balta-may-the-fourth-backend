using StarLs.Application.Dto;
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
    public List<CharacterDto> Characters { get; private set; } = null!;
    public List<PlanetDto> Planets { get; private set; } = null!;
    public List<VehicleDto> Vehicles { get; private set; } = null!;
    public List<StarshipDto> Starships { get; private set; } = null!;
}
