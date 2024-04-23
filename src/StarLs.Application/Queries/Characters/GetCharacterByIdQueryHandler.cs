using AutoMapper;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;

namespace StarLs.Application.Queries.Characters;

public class GetCharacterByIdQueryHandler : IHandler<GetCharacterByIdQueryRequest, GetCharacterByIdQueryResponse>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public GetCharacterByIdQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<GetCharacterByIdQueryResponse> Send(GetCharacterByIdQueryRequest request)
    {
        var data = await _characterRepository.GetByIdAsync(request.Id);

        GetCharacterByIdQueryResponse? response = _mapper.Map<GetCharacterByIdQueryResponse>(data);

        return response;
    }
}
