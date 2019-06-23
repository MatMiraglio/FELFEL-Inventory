using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FELFEL.Domain;
using FELFEL.UseCases.ModifyBatchStock;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.Repositories;
using FELFEL.WebApi.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace FELFEL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly Lazy<IRegisterNewBatch> registerNewBatch;
        private readonly Lazy<IModifyBatchStock> modifyBatchStock;
        private readonly Lazy<IBatchRepository> batchRepository;

        public BatchController(
            Lazy<IBatchRepository> batchRepository,
            Lazy<IRegisterNewBatch> registerNewBatch,
            Lazy<IModifyBatchStock> modifyBatchStock
            )
        {
            this.batchRepository = batchRepository;
            this.registerNewBatch = registerNewBatch;
            this.modifyBatchStock = modifyBatchStock;
        }


        // GET api/batch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batch>>> GetAllBatches()
        {
            var batches = await batchRepository.Value.GetBatchesDeatiledAsync();

            return Ok(batches);
        }

        // GET api/batch/5
        [HttpGet("{batchId}")]
        public async Task<IActionResult> GetBatch([FromRoute] uint batchId) 
        {
            Batch batch;
            try
            {
                batch = await batchRepository.Value.GetBatchDeatiledAsync(batchId);
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }

            if (batch == null) return NotFound($"Could not find batch {batchId}");

            return Ok(batch);
        }

        // GET api/batch/history/5
        [HttpGet("history/{batchId}")]
        public async Task<IActionResult> GetBatchHistory([FromRoute] uint batchId)
        {
            Batch batch;
            try
            {
                batch = await batchRepository.Value.GetBatchWithHistoryAsync(batchId);
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }

            if (batch == null) return NotFound($"could not find batch {batchId}");

            return Ok(batch);
        }

        // POST api/batch
        [HttpPost]
        public async Task<IActionResult> RegisterNewBatch([FromBody] NewBatch newBatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var RequestModel = new RegisterNewBatchRequest(newBatch.ProductId, newBatch.Expiration, newBatch.UnitAmount);

            try
            {
                var Response = await registerNewBatch.Value.ExecuteAsync(RequestModel);
                return CreatedAtAction(nameof(GetBatch) , new { Response.Id }, Response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                //TODO: log error
                throw;
            }
        }

        // PATCH api/batch/5
        [HttpPatch("{batchId}")]
        public async Task<IActionResult> ModifyBatchStock([FromRoute] int batchId, [FromBody] BatchStockModification changeRequest)
        {
            var requestModel = new ModifyBatchStockRequest(batchId, changeRequest.NewUnitAmount, changeRequest.ReasonForChange);

            Batch modifiedBatch;

            try
            {
                modifiedBatch = await modifyBatchStock.Value.ExecuteAsync(requestModel);
                return Ok(modifiedBatch);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                //TODO: log error
                throw;
            }
        }
    }
}
