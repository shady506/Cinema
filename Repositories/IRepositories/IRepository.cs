using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        void Edit(T entity);

        void Delete(T entity);
        Task CommitAsync();

        Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>[]? includes = null);

         Task<T?> GetOne(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null);
    }
}
