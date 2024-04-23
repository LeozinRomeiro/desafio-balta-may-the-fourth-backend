namespace StarLs.Application.Queries.Characters;

public class GetCharacterByIdQueryRequest
{
    public GetCharacterByIdQueryRequest(short id)
     => Id = id;

    public short Id { get; set; }
}
