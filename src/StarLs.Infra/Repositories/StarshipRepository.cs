using Microsoft.EntityFrameworkCore;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Infra.Context;

namespace StarLs.Infra.Repositories;
public class StarshipRepository : IStarshipRepository
{
    private readonly AppDbContext _appDbContext;

    public StarshipRepository(AppDbContext appDbContext) =>
        _appDbContext = appDbContext;

    public async Task<IEnumerable<Starship>> GetAsync() =>
            await _appDbContext.Starships
                .Include(x => x.Movies)
                .AsNoTracking()
                .ToListAsync();

    public async Task<Starship?> GetByIdAsync(short id) =>
           await _appDbContext.Starships
                .Include(x => x.Movies)
                .FirstOrDefaultAsync(x => x.Id == id);
}
