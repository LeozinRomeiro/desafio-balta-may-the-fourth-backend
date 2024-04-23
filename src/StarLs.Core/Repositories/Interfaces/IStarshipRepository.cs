using StarLs.Core.Entities;

namespace StarLs.Core.Repositories.Interfaces
{
    public interface IStarshipRepository
    {
        public Task<IEnumerable<Starship>> GetAsync();
        public Task<Starship> GetByIdAsync(short id);
    }
}
