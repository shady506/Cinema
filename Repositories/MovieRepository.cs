using System.Threading.Tasks;

namespace Cinema.Repositories
{
    public class MovieRepository : Repository<Movies>, IMovieRepository
    {

        private ApplicationDbContext _context; // = new();

        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<Movies> movies)
        {
            await _context.Movies.AddRangeAsync(movies);
        }
 
    }
}
