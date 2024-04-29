using NSubstitute;
using StarLs.Application.Queries;
using StarLs.Application.Queries.Planets;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Tests.Builders.Entities;

namespace StarLs.Tests;

public class PlanetTest : BaseTests, IAsyncLifetime
{
    private IPlanetRepository _planetRepository = null!;
    private List<Planet> _planets = [];

    public Task InitializeAsync()
    {
        _planetRepository = Substitute.For<IPlanetRepository>();
        _planets = MockPlanet.Builder();

        short cont = 1;
        _planets.ForEach(x =>
        {
            _planetRepository
            .GetByIdAsync(cont)
            .Returns(_planets.FirstOrDefault(x => x.Id == cont)!);

            cont++;
        });

        var planet = _planets.FirstOrDefault(x => x.Id == 1)!;

        _planetRepository
            .GetAsync()
            .Returns(_planets);

        return Task.CompletedTask;        
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequestWithValidId_ShouldReturn_ResponseWithOnePlanet(short id)
    {
        var handler = new GetPlanetByIdQueryHandler(_planetRepository, GetMapper());
        var request = new GetPlanetByIdQueryRequest(id);

        var result = await handler.Send(request);

        Assert.Equal(result.Name, MockPlanet.Builder().Find(x => x.Id == id)!.Name);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyPlanet(short id)
    {  
        var handler = new GetPlanetQueryHandler(_planetRepository, GetMapper());
        var request = new GetPlanetQueryRequest();

        var result = await handler.Send(request);

        Assert.Equal(result.FirstOrDefault(x => x.Id == id)!.Name, _planets.FirstOrDefault(x => x.Id == id)!.Name);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyPlanet_WithPagination(short pageNumber, short pageSize)
    {
        var handler = new GetPlanetQueryHandler(_planetRepository, GetMapper());
        var request = new GetPlanetQueryRequest();

        var resultHandler = await handler.Send(request);
        var result = new QueryResult<GetPlanetQueryResponse>(pageNumber, pageSize, resultHandler).Results!;

        Assert.Equal(pageSize, result.Count);
    }
}