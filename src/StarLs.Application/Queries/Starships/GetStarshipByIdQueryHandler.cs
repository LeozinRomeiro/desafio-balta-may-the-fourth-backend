using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

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
        var data = await _starshipRepository.GetByIdAsync(request.Id);

        GetStarshipByIdQueryResponse? response = _mapper.Map<GetStarshipByIdQueryResponse>(data);

        return response;
    }
}
