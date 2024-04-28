using NSubstitute;
using StarLs.Application.Queries.Movies;
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

        var result = await handler.Send(request);

        Assert.Equal(result.Title,MockMovie.Builder().Find(x => x.Id == id)!.Title);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyMovie(short id)
    {  
        var handler = new GetMovieQueryHandler(_movieRepository, GetMapper());
        var request = new GetMovieQueryRequest();

        var result = await handler.Send(request);

        Assert.Equal(result.FirstOrDefault(x => x.Id == id)!.Title, _movies.FirstOrDefault(x => x.Id == id)!.Title);
    }
}