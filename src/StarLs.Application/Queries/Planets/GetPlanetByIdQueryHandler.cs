using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

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
        GetPlanetByIdQueryResponse? response; 

        try
        {
            var data = await _planetRepository.GetByIdAsync(request.Id);

            if(data == null)
                throw new EntityNotFoundException("Entity Not Found");

            response = _mapper.Map<GetPlanetByIdQueryResponse>(data);
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
