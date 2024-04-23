using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

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
        var data = await _starshipRepository.GetAsync();
        List<GetStarshipQueryResponse>? response = _mapper.Map<List<GetStarshipQueryResponse>>(data);

        return response;
    }
}
