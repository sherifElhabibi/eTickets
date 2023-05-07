using eTickets.Models;

namespace eTickets.Data.Base
{
    public interface IEntitiyBaseRepository<T> where T : class,IEntitiyBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int Id);
        Task AddAsync(T entity);
        Task UpdateAsync(int Id, T entity);
        Task DeleteAsync(int Id);
    }
}
