using StarLs.Core.Entities;

namespace StarLs.Core.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        public Task<IEnumerable<Character>> GetAsync(int skip, int take);
        public Task<Character> GetByIdAsync(short id);
    }
}
