using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

namespace StarLs.Application.Queries.Vehicles;

public class GetVehicleByIdQueryHandler : IHandler<GetVehicleByIdQueryRequest, GetVehicleByIdQueryResponse>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;

    public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }

    public async Task<GetVehicleByIdQueryResponse> Send(GetVehicleByIdQueryRequest request)
    {       
        GetVehicleByIdQueryResponse? response; 

        try
        {
            var data = await _vehicleRepository.GetByIdAsync(request.Id);

            if (data == null)
                throw new EntityNotFoundException("Entity Not Found");

            response = _mapper.Map<GetVehicleByIdQueryResponse>(data);
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
