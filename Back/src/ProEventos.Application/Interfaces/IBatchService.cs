using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces
{
    public interface IBatchService
    {
        Task<BatchDto[]> SaveBatch(int eventId, BatchDto[] models);
        Task<bool> DeleteBatch(int eventId, int batchId);

        //GETS
        Task<BatchDto[]> GetAllBatchesByEventIdAsync(int eventId);
        Task<BatchDto> GetBatchByIdAsync(int eventId, int batchId);
    }
}
