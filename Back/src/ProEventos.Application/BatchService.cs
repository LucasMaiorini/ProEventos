using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class BatchService : IBatchService
    {
        private readonly IGeneralPersistence _generalPersistence;
        private readonly IBatchPersistence _batchPersistence;
        private readonly IMapper _mapper;

        public BatchService(
            IGeneralPersistence generalPersistence,
            IBatchPersistence batchPersistence,
            IMapper mapper
            )
        {
            this._generalPersistence = generalPersistence;
            this._batchPersistence = batchPersistence;
            this._mapper = mapper;
        }

        public async Task<BatchDto[]> SaveBatch(int eventId, BatchDto[] models)
        {
            try
            {
                //Takes all batches from a given Event
                Batch[] batches = await _batchPersistence.GetAllBatchesByEventIdAsync(eventId);
                if (batches == null) return null;

                foreach (BatchDto model in models)
                {
                    if (model.Id < 1)
                    {
                        Batch batchToAdd = _mapper.Map<Batch>(model);
                        batchToAdd.EventId = eventId;
                        _generalPersistence.Add<Batch>(batchToAdd);
                        await _generalPersistence.SaveChangesAsync();

                    }
                    //If a batch (model) has an Id, it means that it will be updated, because already exists.
                    else
                    {
                        //Takes the each batch from a given Event.
                        Batch batch = batches.FirstOrDefault(b => b.Id == model.Id);
                        model.EventId = eventId;
                        //Pass to the batch the new values contained in the model.
                        _mapper.Map(model, batch);
                        //Updates it.
                        _generalPersistence.Update<Batch>(batch);
                        await _generalPersistence.SaveChangesAsync();
                    }
                }
                var batchReturn = await _batchPersistence.GetAllBatchesByEventIdAsync(eventId);
                return _mapper.Map<BatchDto[]>(batchReturn);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
        public async Task<bool> DeleteBatch(int eventId, int batchId)
        {
            try
            {
                Batch batch = await _batchPersistence.GetBatchByIdAsync(eventId, batchId);
                if (batch == null) throw new Exception("Lote não encontrado.");

                _generalPersistence.Delete<Batch>(batch);
                return await _generalPersistence.SaveChangesAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<BatchDto[]> GetAllBatchesByEventIdAsync(int eventId)
        {
            try
            {
                Batch[] batches = await _batchPersistence.GetAllBatchesByEventIdAsync(eventId);
                if (batches == null) return null;

                BatchDto[] result = _mapper.Map<BatchDto[]>(batches);
                return result;

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<BatchDto> GetBatchByIdAsync(int eventId, int batchId)
        {
            try
            {
                Batch batch = await _batchPersistence.GetBatchByIdAsync(eventId, batchId);
                if (batch == null) return null;

                BatchDto result = _mapper.Map<BatchDto>(batch);
                return result;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
