using NSubstitute;
using StarLs.Application.Queries.Characters;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Tests.Builders.Entities;

namespace StarLs.Tests;

public class CharacterTest : BaseTests, IAsyncLifetime
{
    private ICharacterRepository _characterRepository = null!;
    private List<Character> _characters = [];

    public Task InitializeAsync()
    {
        _characterRepository = Substitute.For<ICharacterRepository>();
        _characters = MockCharacter.Builder();

        short cont = 1;
        _characters.ForEach(x =>
        {
            _characterRepository
            .GetByIdAsync(cont)
            .Returns(_characters.FirstOrDefault(x => x.Id == cont)!);

            cont++;
        });

        var character = _characters.FirstOrDefault(x => x.Id == 1)!;

        _characterRepository
            .GetAsync()
            .Returns(_characters);

        return Task.CompletedTask;        
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequestWithValidId_ShouldReturn_ResponseWithOneCharacter(short id)
    {
        var handler = new GetCharacterByIdQueryHandler(_characterRepository, GetMapper());
        var request = new GetCharacterByIdQueryRequest(id);

        var result = await handler.Send(request);

        Assert.Equal(result.Name, MockCharacter.Builder().Find(x => x.Id == id)!.Name);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void GivenAValidRequest_ShouldReturn_ResponseWithAnyCharacter(short id)
    {  
        var handler = new GetCharacterQueryHandler(_characterRepository, GetMapper());
        var request = new GetCharacterQueryRequest();

        var result = await handler.Send(request);

        Assert.Equal(result.FirstOrDefault(x => x.Id == id)!.Name, _characters.FirstOrDefault(x => x.Id == id)!.Name);
    }
}