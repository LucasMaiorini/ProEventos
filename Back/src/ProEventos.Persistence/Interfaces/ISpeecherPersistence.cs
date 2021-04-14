using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface ISpeecherPersistence
    {
        //SPEECHERS
        Task<Speecher[]> GetAllSpeechersByNameAsync(string name, bool includeEvents);
        Task<Speecher[]> GetAllSpeechersAsync(bool includeEvents);
        Task<Speecher> GetSpeechersByIdAsync(int id, bool includeEvents);
    }
}