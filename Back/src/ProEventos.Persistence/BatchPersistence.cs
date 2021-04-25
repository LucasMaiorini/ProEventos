using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class BatchPersistence : IBatchPersistence
    {
        private ProEventosContext _context { get; }
        public BatchPersistence(ProEventosContext context)
        {
            this._context = context;
            // this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Batch> GetBatchByIdAsync(int eventId, int batchId)
        {
            IQueryable<Batch> query = _context.Batches;

            query = query.AsNoTracking().Where(b => b.EventId == eventId && b.Id == batchId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Batch[]> GetAllBatchesByEventIdAsync(int eventId)
        {
            IQueryable<Batch> query = _context.Batches;

            query = query.AsNoTracking().Where(b => b.EventId == eventId).OrderBy(b => b.Id);

            return await query.ToArrayAsync();
        }
    }
}
