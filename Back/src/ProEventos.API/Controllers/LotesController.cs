using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Application.Interfaces;

namespace ProBatchos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly IBatchService _batchService;
        private readonly IEventService _eventService;
        public LotesController(IBatchService batchService, IEventService eventService)
        {
            this._batchService = batchService;
            this._eventService = eventService;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(int eventId)
        {
            try
            {
                BatchDto[] batches = await _batchService.GetAllBatchesByEventIdAsync(eventId);
                //IActionResult allows to return the status code, such as Not Found (404)
                if (batches == null) return NoContent();
                //IActionResult allows to return the status code, such as Ok (200)
                return Ok(batches);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar recuperar lotes. Erro: {ex.Message}");
            }
        }


        [HttpPut("{eventId}")]
        public async Task<IActionResult> SaveBatch(int eventId, BatchDto[] models)
        {
            try
            {
                BatchDto[] batchesToUpdate = await _batchService.SaveBatch(eventId, models);
                //IActionResult allows to return the status code, such as BadRequest (400)
                if (batchesToUpdate == null) return NoContent();
                //IActionResult allows to return the status code, such as Ok (200)
                return Ok(batchesToUpdate);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar atualizar lotes. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventId}/{batchId}")]
        public async Task<ActionResult> Delete(int eventId, int batchId)
        {
            try
            {
                BatchDto batchToDelete = await _batchService.GetBatchByIdAsync(eventId, batchId);
                if (batchToDelete == null) return NoContent();

                return await _batchService.DeleteBatch(batchToDelete.EventId, batchToDelete.Id)
                ? Ok(new { messa = "Lote Deletado" }) : throw new Exception("Ocorreu um problema ao deletar o lote.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar excluir batchos. Erro: {ex.Message}");
            }
        }
    }
}
