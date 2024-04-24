using AutoMapper;
using StarLs.Core.Exceptions;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using System.Data.Common;

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
        GetCharacterByIdQueryResponse? response;
        try
        {
            var data = await _characterRepository.GetByIdAsync(request.Id);

            if (data == null)
                throw new EntityNotFoundException("Entity Not found");

            response = _mapper.Map<GetCharacterByIdQueryResponse>(data);

        }
        catch (DbException ex)
        {
            throw new DatabaseException(ex.Message);
        }
        catch (EntityNotFoundException ex)
        {
            throw new EntityNotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return response;
    }
}
