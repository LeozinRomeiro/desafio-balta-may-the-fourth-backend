using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

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
        var data = await _vehicleRepository.GetAsync();
        List<GetVehicleQueryResponse>? response = _mapper.Map<List<GetVehicleQueryResponse>>(data);

        return response;
    }
}
