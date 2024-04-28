using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

namespace StarLs.Application.Queries.Starships;

public class GetStarshipQueryHandler : IHandler<GetStarshipQueryRequest, List<GetStarshipQueryResponse>>
{
    private readonly IStarshipRepository _starshipRepository;
    private readonly IMapper _mapper;

    public GetStarshipQueryHandler(IStarshipRepository starshipRepository, IMapper mapper)
    {
        _starshipRepository = starshipRepository;
        _mapper = mapper;
    }

    public async Task<List<GetStarshipQueryResponse>> Send(GetStarshipQueryRequest request)
    {        
        List<GetStarshipQueryResponse>? response; 

        try
        {
            var data = await _starshipRepository.GetAsync();
            response = _mapper.Map<List<GetStarshipQueryResponse>>(data);
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
