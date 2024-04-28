using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

namespace StarLs.Application.Queries.Starships;

public class GetStarshipByIdQueryHandler : IHandler<GetStarshipByIdQueryRequest, GetStarshipByIdQueryResponse>
{

    private readonly IStarshipRepository _starshipRepository;
    private readonly IMapper _mapper;

    public GetStarshipByIdQueryHandler(IStarshipRepository starshipRepository, IMapper mapper)
    {
        _starshipRepository = starshipRepository;
        _mapper = mapper;
    }

    public async Task<GetStarshipByIdQueryResponse> Send(GetStarshipByIdQueryRequest request)
    {
        GetStarshipByIdQueryResponse? response; 

        try
        {
            var data = await _starshipRepository.GetByIdAsync(request.Id);

            if (data == null)
                throw new EntityNotFoundException("Entity Not Found");

            response = _mapper.Map<GetStarshipByIdQueryResponse>(data);
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
