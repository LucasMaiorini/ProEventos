using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersistence _generalPersistence;
        private readonly IEventPersistence _eventPersistence;
        public EventService(IGeneralPersistence generalPersistence, IEventPersistence eventPersistence)
        {
            this._generalPersistence = generalPersistence;
            this._eventPersistence = eventPersistence;
        }
        public async Task<Event> AddEvents(Event model)
        {
            try
            {
                _generalPersistence.Add<Event>(model);
                if (await _generalPersistence.SaveChangesAsync())
                {
                    //The event is returned just in case the dev wants to use it for something in project.
                    return await _eventPersistence.GetEventByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }


        public async Task<Event> UpdateEvent(int id, Event model)
        {
            try
            {
                //This GetEventByIdAsync holds the Entity and untill it is finished, it can't be updated.
                //This is the reason we need to add AsNoTracking
                Event eventToUpdate = await _eventPersistence.GetEventByIdAsync(id);
                if (eventToUpdate == null) return null;

                model.Id = eventToUpdate.Id;

                _generalPersistence.Update(model);
                if (await _generalPersistence.SaveChangesAsync())
                {
                    //The event is returned just in case the dev wants to use it for something in project.
                    return await _eventPersistence.GetEventByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }


        public async Task<bool> DeleteEvent(int id)
        {
            try
            {
                Event eventToDelete = await _eventPersistence.GetEventByIdAsync(id);
                if (eventToDelete == null) throw new Exception("Evento para remover não foi encontrado.");

                _generalPersistence.Delete<Event>(eventToDelete);
                return await _generalPersistence.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<Event> GetEventByIdAsync(int id, bool includeSpeechers = false)
        {
            try
            {
                Event eventToReturn = await _eventPersistence.GetEventByIdAsync(id);
                if (eventToReturn == null) return null;

                return eventToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeechers = false)
        {
            try
            {
                Event[] events = await _eventPersistence.GetAllEventsAsync();
                if (events == null) return null;

                return events;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeechers = false)
        {
            try
            {
                Event[] events = await _eventPersistence.GetAllEventsByThemeAsync(theme);
                if (events == null) return null;

                return events;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

    }
}
