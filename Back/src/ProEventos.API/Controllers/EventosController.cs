using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventosController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Event[] events = await _eventService.GetAllEventsAsync(true);
                //IActionResult allows to return the status code, such as Not Found (404)
                if (events == null) return NotFound("Nenhum Evento encontrado.");
                //IActionResult allows to return the status code, such as Ok (200)
                return Ok(events);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetById(int id)
        {
            try
            {
                Event eventToReturn = await _eventService.GetEventByIdAsync(id, true);
                //IActionResult allows to return the status code, such as Not Found (404)
                if (eventToReturn == null) return NotFound("Nenhum Evento encontrado.");
                //IActionResult allows to return the status code, such as Ok (200)
                return Ok(eventToReturn);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("theme/{theme}")]
        public async Task<ActionResult<Event[]>> GetByTheme(string theme)
        {
            try
            {
                Event[] events = await _eventService.GetAllEventsByThemeAsync(theme, true);
                //IActionResult allows to return the status code, such as Not Found (404)
                if (events == null) return NotFound("Nenhum Evento encontrado.");
                //IActionResult allows to return the status code, such as Ok (200)
                return Ok(events);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Event model)
        {
            try
            {
                Event eventToPost = await _eventService.AddEvents(model);
                //IActionResult allows to return the status code, such as BadRequest (400)
                if (eventToPost == null) return BadRequest("Erro ao tentar adicionar o evento.");
                //IActionResult allows to return the status code, such as Ok (200)
                return Ok(eventToPost);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Event model)
        {
            try
            {
                Event eventToUpdate = await _eventService.UpdateEvent(id, model);
                //IActionResult allows to return the status code, such as BadRequest (400)
                if (eventToUpdate == null) return BadRequest("Erro ao tentar adicionar o evento.");
                //IActionResult allows to return the status code, such as Ok (200)
                return Ok(eventToUpdate);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar atualizar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                if (await _eventService.DeleteEvent(id))
                {
                    return Ok("Deletado");
                }
                else
                {
                    return BadRequest("Não foi possível deletar o evento.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar excluir eventos. Erro: {ex.Message}");
            }
        }
    }
}
