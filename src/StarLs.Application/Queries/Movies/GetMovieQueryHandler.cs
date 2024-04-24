using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

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

        List<GetMovieQueryResponse>? response;

        try
        {
            var data = await _movieRepository.GetAsync();
            response = _mapper.Map<List<GetMovieQueryResponse>>(data);
        }
        catch (DbException ex)
        {
            throw new DatabaseException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return response;
    }
}
