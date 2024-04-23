namespace StarLs.Application.Queries.Planets;

public class GetPlanetByIdQueryRequest
{
    public GetPlanetByIdQueryRequest(short id)
        => Id = id;

    public short Id { get; set; }
}
