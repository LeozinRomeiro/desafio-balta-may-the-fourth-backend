using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

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

            if (data == null)
                throw new EntityNotFoundException("Entity Not Found");

            response = _mapper.Map<GetMovieByIdQueryResponse>(data);
        }
        catch (DbException ex)
        {
            throw new DatabaseException(ex.Message);
        }
        catch (EntityNotFoundException ex)
        {
            throw new EntityNotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return response;
    }
}
