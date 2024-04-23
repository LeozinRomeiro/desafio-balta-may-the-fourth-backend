namespace StarLs.Application.Queries.Vehicles;

public class GetVehicleByIdQueryRequest
{
    public GetVehicleByIdQueryRequest(short id)
        => Id = id;    

    public short Id { get; set; }
}
