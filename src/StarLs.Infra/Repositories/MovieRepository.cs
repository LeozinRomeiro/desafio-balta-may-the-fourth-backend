using Microsoft.EntityFrameworkCore;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Infra.Context;

namespace StarLs.Infra.Repositories;
public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _appDbContext;

    public MovieRepository(AppDbContext appDbContext) => 
        _appDbContext = appDbContext;
    
    public async Task<IEnumerable<Movie>> GetAsync() =>
        await _appDbContext.Movies
            .Include(x => x.Characters)
            .Include(x => x.Planets)
            .Include(x => x.Vehicles)
            .Include(x => x.Starships)
            .AsNoTracking()
            .ToListAsync();
    
    public async Task<Movie?> GetByIdAsync(short id) =>
        await _appDbContext.Movies
            .Include(x => x.Characters)
            .Include(x => x.Planets)
            .Include(x => x.Vehicles)
            .Include(x => x.Starships)
            .FirstOrDefaultAsync(x => x.Id == id);
}
