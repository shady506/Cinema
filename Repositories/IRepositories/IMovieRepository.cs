using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories.IRepositories
{
    public interface IMovieRepository : IRepository<Movies>
    {
        Task AddRangeAsync(List<Movies> movies);
    }
}
