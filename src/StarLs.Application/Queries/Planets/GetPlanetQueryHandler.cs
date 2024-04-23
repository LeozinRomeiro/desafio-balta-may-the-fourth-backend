using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

namespace StarLs.Application.Queries.Planets;

public class GetPlanetQueryHandler : IHandler<GetPlanetQueryRequest, List<GetPlanetQueryResponse>>
{
    private readonly IPlanetRepository _planetRepository;
    private readonly IMapper _mapper;

    public GetPlanetQueryHandler(IPlanetRepository planetRepository, IMapper mapper)
    {
        _planetRepository = planetRepository;
        _mapper = mapper;
    }

    public async Task<List<GetPlanetQueryResponse>> Send(GetPlanetQueryRequest request)
    {
        var data = await _planetRepository.GetAsync();
        List<GetPlanetQueryResponse>? response = _mapper.Map<List<GetPlanetQueryResponse>>(data);

        return response;
    }
}
