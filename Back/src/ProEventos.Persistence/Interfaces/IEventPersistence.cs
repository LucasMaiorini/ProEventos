using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventPersistence
    {
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeechers = false);
        Task<Event[]> GetAllEventsAsync(bool includeSpeechers = false);
        Task<Event> GetEventByIdAsync(int id, bool includeSpeechers = false);
    }
}
