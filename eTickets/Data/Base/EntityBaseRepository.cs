namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntitiyBaseRepository<T> where T : class,IEntitiyBase, new()
    {
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(int Id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
