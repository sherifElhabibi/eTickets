using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly eTicketContext _context;
        public ActorsService(eTicketContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            var result = await _context.Actor.ToListAsync();
            return result;
        }

        public async Task<Actor> GetByIdAsync(int ActorId)
        {
            var result = await _context.Actor.FirstOrDefaultAsync(a=>a.ActorId==ActorId);
            return result;
        }

        public async Task<Actor> UpdateAsync(int ActorId, Actor newActor)
        {
            _context.Actor.Update(newActor);
            await _context.SaveChangesAsync();
            return newActor;
        }
        public async Task DeleteAsync(int ActorId)
        {
            var result = await _context.Actor.FirstOrDefaultAsync(a => a.ActorId == ActorId);
            _context.Actor.Remove(result);
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(Actor actor)
        {
            await _context.Actor.AddAsync(actor);
            await _context.SaveChangesAsync();
            
        }
    }
}
