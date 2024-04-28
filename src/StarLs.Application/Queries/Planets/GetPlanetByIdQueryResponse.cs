using StarLs.Application.Dto;
using StarLs.Core.Entities;

namespace StarLs.Application.Queries.Planets;

public class GetPlanetByIdQueryResponse
{
    public short Id { get; set; }
    public string Name { get; private set; } = null!;
    public string RotationPeriod { get; private set; } = null!;
    public string OrbitalPeriod { get; private set; } = null!;
    public string Diameter { get; private set; } = null!;
    public string Climate { get; private set; } = null!;
    public string Gravity { get; private set; } = null!;
    public string Terrain { get; private set; } = null!;
    public string SurfaceWater { get; private set; } = null!;
    public string Population { get; private set; } = null!;
    public List<CharacterDto> Characters { get; private set; } = null!;
    public List<MovieDto> Movies { get; private set; } = null!;
}
