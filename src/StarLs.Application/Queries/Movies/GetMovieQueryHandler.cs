using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

namespace StarLs.Application.Queries.Movies;

public class GetMovieQueryHandler : IHandler<GetMovieQueryResquest, List<GetMovieQueryResponse>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetMovieQueryHandler(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<List<GetMovieQueryResponse>> Send(GetMovieQueryResquest request)
    {
        var data = await _movieRepository.GetAsync();
        List<GetMovieQueryResponse>? response = _mapper.Map<List<GetMovieQueryResponse>>(data);

        return response;
    }
}
