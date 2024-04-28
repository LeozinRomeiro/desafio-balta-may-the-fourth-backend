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

    public async Task<IEnumerable<Character>>? GetAsync(int skip, int take) =>
          await _appDbContext.Characters
            .Include(x => x.Planet)
            .Include(x => x.Movies)
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Character?> GetByIdAsync(short id) =>
          await _appDbContext.Characters
            .Include(x => x.Planet)
            .Include(x => x.Movies)
            .FirstOrDefaultAsync(x => x.Id == id);
}
