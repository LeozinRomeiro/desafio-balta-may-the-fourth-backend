using Microsoft.EntityFrameworkCore;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Infra.Context;

namespace StarLs.Infra.Repositories;
public class PlanetRepository : IPlanetRepository
{
    private readonly AppDbContext _appDbContext;

    public PlanetRepository(AppDbContext appDbContext) =>
        _appDbContext = appDbContext;

    public async Task<IEnumerable<Planet>> GetAsync() =>
        await _appDbContext.Planets
            .Include(x => x.Characters)
            .Include(x => x.Movies)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Planet?> GetByIdAsync(short id) =>
           await _appDbContext.Planets
            .Include(x => x.Characters)
            .Include(x => x.Movies)
            .FirstOrDefaultAsync(x => x.Id == id);
}
