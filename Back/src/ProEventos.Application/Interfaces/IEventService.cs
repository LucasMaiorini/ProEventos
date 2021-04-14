using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Interfaces
{
    public interface IEventService
    {
        Task<Event> AddEvents(Event model);
        Task<Event> UpdateEvent(int id, Event model);
        Task<bool> DeleteEvent(int id);

        //GETS
        Task<Event[]> GetAllEventsAsync(bool includeSpeechers = false);
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeechers = false);
        Task<Event> GetEventByIdAsync(int id, bool includeSpeechers = false);
    }
}
