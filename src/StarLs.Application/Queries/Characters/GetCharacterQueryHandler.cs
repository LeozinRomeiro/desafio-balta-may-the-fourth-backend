using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

namespace StarLs.Application.Queries.Characters;

public class GetCharacterQueryHandler : IHandler<GetCharacterQueryRequest, List<GetCharacterQueryResponse>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public GetCharacterQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<List<GetCharacterQueryResponse>> Send(GetCharacterQueryRequest request)
    {
        List<GetCharacterQueryResponse>? response;
        try
        {
            var data = await _characterRepository.GetAsync();
            response = _mapper.Map<List<GetCharacterQueryResponse>>(data);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return response;
    }
}
