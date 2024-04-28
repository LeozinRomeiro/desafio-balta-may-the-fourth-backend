using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

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
        
        List<GetPlanetQueryResponse>? response; 
        try
        {
            var data = await _planetRepository.GetAsync();
            response = _mapper.Map<List<GetPlanetQueryResponse>>(data);
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

    public Task<List<GetPlanetQueryResponse>> Send(GetPlanetQueryRequest request, int skip, int take)
    {
        throw new NotImplementedException();
    }
}
