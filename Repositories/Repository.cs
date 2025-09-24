using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cinema.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private ApplicationDbContext _context; // = new();
        private DbSet<T> _db;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _db.AddAsync(entity);
        }
        public void Edit(T entity)
        {
            _db.Update(entity);
          
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>[] ? includes = null)
        {
            var entity = _db.AsQueryable();
            if (expression is not null)
            {
                 entity = entity.Where(expression);
            }

            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    entity = entity.Include(item);
                }
                
            }

            return await entity.ToListAsync();
            
        }

        public async Task<T?> GetOne(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null)
        {
           
            return (await GetAsync(expression, includes)).FirstOrDefault();

        }

    }
}
