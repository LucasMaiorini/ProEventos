using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IBatchPersistence
    {
        Task<Batch[]> GetAllBatchesByEventIdAsync(int eventId);
        /// <summary>
        /// Takes 2 ids, one of the Event which it belongs and the other from the batch itself.
        /// </summary>
        /// <param name="eventId">The id from the Event which the batch belongs.</param>
        /// <param name="batchId">The id from the Batch itself</param>
        /// <returns>Returns just 1 Batch</returns>
        Task<Batch> GetBatchByIdAsync(int eventId, int batchId);
    }
}
