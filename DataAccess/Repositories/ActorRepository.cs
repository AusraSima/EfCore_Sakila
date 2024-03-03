using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ActorRepository
    {
        private readonly SakilaDbContext _context;
        public ActorRepository(SakilaDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Actor>> ReadAllAsync()
        {
            return await _context.Actors.Include(actor => actor.Films).ToListAsync();
        }
        public async Task<Actor> ReadAsync(short id)
        {
            return await _context.Actors.Include(a => a.Films).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task UpdateAsync(Actor actor)
        {
            var newActor = await ReadAsync(actor.Id);
            _context.Actors.Entry(newActor).CurrentValues.SetValues(actor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Actor actor)
        {
            var deleteActor = await ReadAsync(actor.Id);
            _context.Actors.Remove(deleteActor);
            await _context.SaveChangesAsync();
        }
    }
}
