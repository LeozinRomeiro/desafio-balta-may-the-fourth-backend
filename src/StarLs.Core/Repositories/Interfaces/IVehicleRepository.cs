using StarLs.Core.Entities;

namespace StarLs.Core.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        public Task<IEnumerable<Vehicle>> GetAsync();
        public Task<Vehicle> GetByIdAsync(short id);
    }
}
