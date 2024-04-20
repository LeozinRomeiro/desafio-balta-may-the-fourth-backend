using StarLs.Core.Entities;

namespace StarLs.Core.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAsync();
        public Task<Movie> GetByIdAsync(short id);
    }
}
