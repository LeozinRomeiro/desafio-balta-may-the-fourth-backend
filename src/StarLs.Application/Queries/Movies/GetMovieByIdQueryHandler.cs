using AutoMapper;
using StarLs.Core.Exceptions;
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
        GetMovieByIdQueryResponse? response;
        try
        {
            var data = await _movieRepository.GetByIdAsync(request.Id);
            response = _mapper.Map<GetMovieByIdQueryResponse>(data);

            if(response == null)
                throw new NotFoundException("Entidade nao encontrada");

        }
        catch (NotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }       

        return response;
    }
}
