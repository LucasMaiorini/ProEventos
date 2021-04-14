using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class EventPersistence : IEventPersistence
    {
        private ProEventosContext _context { get; }
        public EventPersistence(ProEventosContext context)
        {
            this._context = context;
            // this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Event> GetEventByIdAsync(int id, bool includeSpeechers = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Batches).Include(e => e.SocialNetworks);
            if (includeSpeechers)
            {
                query = query.Include(e => e.SpeecherEvents).ThenInclude(se => se.Speecher);
            }
            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeechers = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Batches).Include(e => e.SocialNetworks);
            if (includeSpeechers)
            {
                query = query.Include(e => e.SpeecherEvents).ThenInclude(se => se.Speecher);
            }
            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeechers = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Batches).Include(e => e.SocialNetworks);
            if (includeSpeechers)
            {
                query = query.Include(e => e.SpeecherEvents).ThenInclude(se => se.Speecher);
            }
            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}
