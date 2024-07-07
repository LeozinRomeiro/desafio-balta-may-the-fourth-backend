using NSubstitute;
using StarLs.Application.Queries.Movies;
using StarLs.Application.Queries;
using StarLs.Application.Queries.Starships;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Tests.Builders.Entities;

namespace StarLs.Tests;

public class StarshipTest : BaseTests, IAsyncLifetime
{
    private IStarshipRepository _starshipRepository = null!;
    private List<Starship> _starships = [];

    public Task InitializeAsync()
    {
        _starshipRepository = Substitute.For<IStarshipRepository>();
        _starships = MockStarship.Builder();

        short cont = 1;
        _starships.ForEach(x =>
        {
            _starshipRepository
            .GetByIdAsync(cont)
            .Returns(_starships.FirstOrDefault(x => x.Id == cont)!);

            cont++;
        });

        var starship = _starships.FirstOrDefault(x => x.Id == 1)!;

        _starshipRepository
            .GetAsync()
            .Returns(_starships);

        return Task.CompletedTask;        
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequestWithValidId_ShouldReturn_ResponseWithOneStarship(short id)
    {
        var handler = new GetStarshipByIdQueryHandler(_starshipRepository, GetMapper());
        var request = new GetStarshipByIdQueryRequest(id);

        var result = await handler.Send(request);

        Assert.Equal(result.Name, MockStarship.Builder().Find(x => x.Id == id)!.Name);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyStarship(short id)
    {  
        var handler = new GetStarshipQueryHandler(_starshipRepository, GetMapper());
        var request = new GetStarshipQueryRequest();

        var resultHandler = await handler.Send(request);

        Assert.Equal(resultHandler.FirstOrDefault(x => x.Id == id)!.Name, _starships.FirstOrDefault(x => x.Id == id)!.Name);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyStarship_WithPagination(short pageNumber, short pageSize)
    {
        var handler = new GetStarshipQueryHandler(_starshipRepository, GetMapper());
        var request = new GetStarshipQueryRequest();

        var resultHandler = await handler.Send(request);
        var result = new QueryResult<GetStarshipQueryResponse>(pageNumber, pageSize, resultHandler).Results!;

        Assert.Equal(pageSize, result.Count);
    }
}