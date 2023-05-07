using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntitiyBaseRepository<T> where T : class,IEntitiyBase, new()
    {
        private readonly eTicketContext _context;
        public EntityBaseRepository(eTicketContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity) 
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
              
         }
        

        public async Task DeleteAsync(int Id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == Id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAll()=> await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int Id)=> await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == Id);

        public async Task UpdateAsync(int Id, T entity)
        {
            EntityEntry entityEntry =  _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
