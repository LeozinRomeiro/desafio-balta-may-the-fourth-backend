using StarLs.Core.Entities;

namespace StarLs.Core.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        public Task<IEnumerable<Character>> GetAsync();
        public Task<Character> GetByIdAsync(short id);
    }
}
