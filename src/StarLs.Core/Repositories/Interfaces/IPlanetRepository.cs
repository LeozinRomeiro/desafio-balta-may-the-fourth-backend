using StarLs.Core.Entities;

namespace StarLs.Core.Repositories.Interfaces
{
    public interface IPlanetRepository
    {
        public Task<IEnumerable<Planet>> GetAsync();
        public Task<Planet> GetByIdAsync(short id);
    }
}
