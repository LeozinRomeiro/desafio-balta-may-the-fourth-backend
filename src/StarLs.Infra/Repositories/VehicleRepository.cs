using Microsoft.EntityFrameworkCore;
using StarLs.Core.Entities;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Infra.Context;

namespace StarLs.Infra.Repositories;
public class VehicleRepository : IVehicleRepository
{
    private readonly AppDbContext _appDbContext;

    public VehicleRepository(AppDbContext appDbContext) =>
        _appDbContext = appDbContext;

    public async Task<IEnumerable<Vehicle>> GetAsync() =>
            await _appDbContext.Vehicles
            .AsNoTracking()
            .ToListAsync();

    public async Task<Vehicle?> GetByIdAsync(short id) =>
           await _appDbContext.Vehicles
           .FirstOrDefaultAsync(x => x.Id == id);
}
