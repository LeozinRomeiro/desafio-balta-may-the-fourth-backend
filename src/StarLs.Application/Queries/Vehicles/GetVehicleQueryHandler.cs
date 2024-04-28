using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

namespace StarLs.Application.Queries.Vehicles;

public class GetVehicleQueryHandler : IHandler<GetVehicleQueryRequest, List<GetVehicleQueryResponse>>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;

    public GetVehicleQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }

    public async Task<List<GetVehicleQueryResponse>> Send(GetVehicleQueryRequest request)
    {
        List<GetVehicleQueryResponse>? response;

        try
        {
            var data = await _vehicleRepository.GetAsync();

            if (data == null)
                throw new EntityNotFoundException("Entity Not Found");

            response = _mapper.Map<List<GetVehicleQueryResponse>>(data);
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
