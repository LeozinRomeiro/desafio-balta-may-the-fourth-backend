using NSubstitute;
using NSubstitute.Core;
using StarLs.Application.Queries;
using StarLs.Application.Queries.Characters;
using StarLs.Application.Queries.Movies;
using StarLs.Application.Queries.Starships;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Tests.Builders.Entities;

namespace StarLs.Tests;

public class MovieTest : BaseTests, IAsyncLifetime
{
    private IMovieRepository _movieRepository = null!;
    private List<Movie> _movies = [];

    public Task InitializeAsync()
    {
        _movieRepository = Substitute.For<IMovieRepository>();
        _movies = MockMovie.Builder();

        short cont = 1;
        _movies.ForEach(x =>
        {
            _movieRepository
            .GetByIdAsync(cont)
            .Returns(_movies.FirstOrDefault(x => x.Id == cont)!);

            cont++;
        });

        var movie = _movies.FirstOrDefault(x => x.Id == 1)!;

        _movieRepository
            .GetAsync()
            .Returns(_movies);

        return Task.CompletedTask;        
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequestWithValidId_ShouldReturn_ResponseWithOneMovie(short id)
    {
        var handler = new GetMovieByIdQueryHandler(_movieRepository, GetMapper());
        var request = new GetMovieByIdQueryRequest(id);

        var resultHandler = await handler.Send(request);
        
        Assert.Equal(resultHandler.Title,MockMovie.Builder().Find(x => x.Id == id)!.Title);
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 1, 2)]
    [InlineData(3, 1, 3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyMovie(short id, short pageNumber, short pageSize)
    {  
        var handler = new GetMovieQueryHandler(_movieRepository, GetMapper());
        var request = new GetMovieQueryRequest();

        var resultHandler = await handler.Send(request);

        Assert.Equal(resultHandler.FirstOrDefault(x => x.Id == id)!.Title, _movies.FirstOrDefault(x => x.Id == id)!.Title);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyMovie_WithPagination(short pageNumber, short pageSize)
    {
        var handler = new GetMovieQueryHandler(_movieRepository, GetMapper());
        var request = new GetMovieQueryRequest();

        var resultHandler = await handler.Send(request);
        var result = new QueryResult<GetMovieQueryResponse>(pageNumber, pageSize, resultHandler).Results!;

        Assert.Equal(pageSize, result.Count);
    }
}