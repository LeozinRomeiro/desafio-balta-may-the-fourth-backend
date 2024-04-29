using NSubstitute;
using StarLs.Application.Queries.Starships;
using StarLs.Application.Queries;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Tests.Builders.Entities;

namespace StarLs.Tests;

public class VehicleTest : BaseTests, IAsyncLifetime
{
    private IVehicleRepository _vehicleRepository = null!;
    private List<Vehicle> _vehicles = [];

    public Task InitializeAsync()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _vehicles = MockVehicle.Builder();

        short cont = 1;
        _vehicles.ForEach(x =>
        {
            _vehicleRepository
            .GetByIdAsync(cont)
            .Returns(_vehicles.FirstOrDefault(x => x.Id == cont)!);

            cont++;
        });

        var vehicle = _vehicles.FirstOrDefault(x => x.Id == 1)!;

        _vehicleRepository
            .GetAsync()
            .Returns(_vehicles);

        return Task.CompletedTask;        
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequestWithValidId_ShouldReturn_ResponseWithOneVehicle(short id)
    {
        var handler = new GetVehicleByIdQueryHandler(_vehicleRepository, GetMapper());
        var request = new GetVehicleByIdQueryRequest(id);

        var result = await handler.Send(request);

        Assert.Equal(result.Name, MockVehicle.Builder().Find(x => x.Id == id)!.Name);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyVehicle(short id)
    {  
        var handler = new GetVehicleQueryHandler(_vehicleRepository, GetMapper());
        var request = new GetVehicleQueryRequest();

        var result = await handler.Send(request);

        Assert.Equal(result.FirstOrDefault(x => x.Id == id)!.Name, _vehicles.FirstOrDefault(x => x.Id == id)!.Name);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyVehicle_WithPagination(short pageNumber, short pageSize)
    {
        var handler = new GetVehicleQueryHandler(_vehicleRepository, GetMapper());
        var request = new GetVehicleQueryRequest();

        var resultHandler = await handler.Send(request);
        var result = new QueryResult<GetVehicleQueryResponse>(pageNumber, pageSize, resultHandler).Results!;

        Assert.Equal(pageSize, result.Count);
    }
}