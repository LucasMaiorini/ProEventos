using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces
{
    public interface IEventService
    {
        Task<EventDto> AddEvents(EventDto model);
        Task<EventDto> UpdateEvent(int id, EventDto model);
        Task<bool> DeleteEvent(int id);

        //GETS
        Task<EventDto[]> GetAllEventsAsync(bool includeSpeechers = false);
        Task<EventDto[]> GetAllEventsByThemeAsync(string theme, bool includeSpeechers = false);
        Task<EventDto> GetEventByIdAsync(int id, bool includeSpeechers = false);
    }
}
