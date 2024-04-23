namespace StarLs.Application.Queries.Movies;

public class GetMovieByIdQueryRequest
{
    public GetMovieByIdQueryRequest(short id)
     => Id = id;
    

    public short Id { get; set; }
}
