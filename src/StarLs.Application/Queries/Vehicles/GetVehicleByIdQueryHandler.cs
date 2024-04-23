using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

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
        var data = await _vehicleRepository.GetByIdAsync(request.Id);

        GetVehicleByIdQueryResponse? response = _mapper.Map<GetVehicleByIdQueryResponse>(data);

        return response;
    }
}
