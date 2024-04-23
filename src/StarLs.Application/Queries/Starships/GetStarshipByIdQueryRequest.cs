namespace StarLs.Application.Queries.Starships;

public class GetStarshipByIdQueryRequest
{
    public GetStarshipByIdQueryRequest(short id)
        => Id = id;

    public short Id { get; set; }
}
