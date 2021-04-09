using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        public IEnumerable<Event> _events = new Event[]{

           new Event(){
               EventId = 1,
               EventDate = DateTime.Now.AddDays(2).ToString(),
               Theme = "Angular 11 & .NET 5",
               Location = "Belo Horizonte",
               Batch = "1º batch",
                  NumberOfPeople = 250,
               ImageURL = ""
             },
             new Event(){
               EventId = 2,
               EventDate = DateTime.Now.AddDays(10).ToString(),
               Theme = "Angular 10 & .NET 5",
               Location = "Belo Horizonte",
               Batch = "2º batch",
               NumberOfPeople = 250,
               ImageURL = ""
             },
        };

        public EventoController()
        {
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _events;
        }

        [HttpGet("{id}")]
        public IEnumerable<Event> Get(int id)
        {
            return _events.Where(e => e.EventId == id);
        }
    }
}
