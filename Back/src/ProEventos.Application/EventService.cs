using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersistence _generalPersistence;
        private readonly IEventPersistence _eventPersistence;
        private readonly IMapper _mapper;

        public EventService(
            IGeneralPersistence generalPersistence,
            IEventPersistence eventPersistence,
            IMapper mapper
            )
        {
            this._generalPersistence = generalPersistence;
            this._eventPersistence = eventPersistence;
            this._mapper = mapper;
        }
        public async Task<EventDto> AddEvents(EventDto model)
        {
            try
            {
                Event mappedEvent = _mapper.Map<Event>(model);
                _generalPersistence.Add<Event>(mappedEvent);
                if (await _generalPersistence.SaveChangesAsync())
                {
                    //The eventDto is returned just in case the dev wants to use it for something in project.
                    Event unmappedEvent = await _eventPersistence.GetEventByIdAsync(mappedEvent.Id);
                    EventDto mappedEventDto = _mapper.Map<EventDto>(unmappedEvent);
                    return mappedEventDto;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<EventDto> UpdateEvent(int id, EventDto eventDto)
        {
            try
            {
                //This GetEventByIdAsync holds the Entity and untill it is finished, it can't be updated.
                //This is the reason we need to add AsNoTracking
                Event eventToUpdate = await _eventPersistence.GetEventByIdAsync(id);
                if (eventToUpdate == null) return null;
                eventDto.Id = eventToUpdate.Id;
                //The Map takes all eventDto fields values and pass it to eventToUpdate
                _mapper.Map(eventDto, eventToUpdate);
                _generalPersistence.Update<Event>(eventToUpdate);
                if (await _generalPersistence.SaveChangesAsync())
                {
                    var eventToReturn = await _eventPersistence.GetEventByIdAsync(eventToUpdate.Id);
                    //The event is returned just in case the dev wants to use it for something in project.
                    return _mapper.Map<EventDto>(eventToReturn);
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

        public async Task<EventDto> GetEventByIdAsync(int id, bool includeSpeechers = false)
        {
            try
            {
                Event unmappedEvent = await _eventPersistence.GetEventByIdAsync(id);
                if (unmappedEvent == null) return null;
                EventDto mappedEvent = _mapper.Map<EventDto>(unmappedEvent);
                return mappedEvent;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<EventDto[]> GetAllEventsAsync(bool includeSpeechers = false)
        {
            try
            {
                Event[] unmappedEvents = await _eventPersistence.GetAllEventsAsync();
                if (unmappedEvents == null) return null;
                EventDto[] mappedEvents = _mapper.Map<EventDto[]>(unmappedEvents);
                return mappedEvents;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<EventDto[]> GetAllEventsByThemeAsync(string theme, bool includeSpeechers = false)
        {
            try
            {
                Event[] unmappedEvents = await _eventPersistence.GetAllEventsByThemeAsync(theme);
                if (unmappedEvents == null) return null;

                EventDto[] mappedEvents = _mapper.Map<EventDto[]>(unmappedEvents);
                return mappedEvents;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

    }
}
