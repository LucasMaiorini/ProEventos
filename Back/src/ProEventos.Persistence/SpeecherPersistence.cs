using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class SpeecherPersistence : ISpeecherPersistence
    {
        private ProEventosContext _context { get; }
        public SpeecherPersistence(ProEventosContext context)
        {
            this._context = context;
            // this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Speecher[]> GetAllSpeechersAsync(bool includeEvents = false)
        {
            IQueryable<Speecher> query = _context.Speechers.Include(s => s.SocialNetworks);
            if (includeEvents)
            {
                query = query.Include(s => s.SpeecherEvents).ThenInclude(se => se.Event);
            }
            query = query.AsNoTracking().OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Speecher[]> GetAllSpeechersByNameAsync(string name, bool includeEvents = false)
        {
            IQueryable<Speecher> query = _context.Speechers.Include(s => s.SocialNetworks);
            if (includeEvents)
            {
                query = query.Include(s => s.SpeecherEvents).ThenInclude(se => se.Event);
            }
            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Speecher> GetSpeechersByIdAsync(int id, bool includeEvents = false)
        {
            IQueryable<Speecher> query = _context.Speechers.Include(s => s.SocialNetworks);
            if (includeEvents)
            {
                query = query.Include(s => s.SpeecherEvents).ThenInclude(se => se.Event);
            }
            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
