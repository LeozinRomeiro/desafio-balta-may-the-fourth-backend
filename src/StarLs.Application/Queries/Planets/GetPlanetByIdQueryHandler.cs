using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

namespace StarLs.Application.Queries.Planets;

public class GetPlanetByIdQueryHandler : IHandler<GetPlanetByIdQueryRequest, GetPlanetByIdQueryResponse>
{
    private readonly IPlanetRepository _planetRepository;
    private readonly IMapper _mapper;

    public GetPlanetByIdQueryHandler(IPlanetRepository planetRepository, IMapper mapper)
    {
        _planetRepository = planetRepository;
        _mapper = mapper;
    }

    public async Task<GetPlanetByIdQueryResponse> Send(GetPlanetByIdQueryRequest request)
    {
        var data = await _planetRepository.GetByIdAsync(request.Id);

        GetPlanetByIdQueryResponse? response = _mapper.Map<GetPlanetByIdQueryResponse>(data);

        return response;
    }
}
