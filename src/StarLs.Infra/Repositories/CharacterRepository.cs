using Microsoft.EntityFrameworkCore;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Infra.Context;

namespace StarLs.Infra.Repositories;
public class CharacterRepository : ICharacterRepository
{
    private readonly AppDbContext _appDbContext;

    public CharacterRepository(AppDbContext appDbContext) =>
        _appDbContext = appDbContext;

    public async Task<IEnumerable<Character>> GetAsync() =>
          await _appDbContext.Characters
          .AsNoTracking()
          .ToListAsync();

    public async Task<Character?> GetByIdAsync(short id) =>
          await _appDbContext.Characters
          .FirstOrDefaultAsync(x => x.Id == id);
}
