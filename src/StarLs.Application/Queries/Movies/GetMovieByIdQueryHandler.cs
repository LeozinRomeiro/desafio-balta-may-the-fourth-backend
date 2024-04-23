using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

namespace StarLs.Application.Queries.Movies;

public class GetMovieByIdQueryHandler : IHandler<GetMovieByIdQueryRequest, GetMovieByIdQueryResponse>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetMovieByIdQueryHandler(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<GetMovieByIdQueryResponse> Send(GetMovieByIdQueryRequest request)
    {
        var data = await _movieRepository.GetByIdAsync(request.Id);

        GetMovieByIdQueryResponse? response = _mapper.Map<GetMovieByIdQueryResponse>(data);

        return response;
    }
}
