using AutoMapper;
using StarLs.Core.Entities;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

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
        catch (DbException ex)
        {
            throw new DatabaseException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return response;
    }
}
