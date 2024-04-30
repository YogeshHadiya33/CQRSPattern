using System.Linq.Expressions;

namespace CQRSPattern.Repository.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetAsync(int id);
    Task<T> UpdateAsync(T entity);
}