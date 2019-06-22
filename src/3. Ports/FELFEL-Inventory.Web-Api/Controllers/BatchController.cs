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
        private readonly IRegisterNewBatch registerNewBatch;
        private readonly IModifyBatchStock modifyBatchStock;
        private readonly IBatchRepository batchRepository;

        public BatchController(
            IBatchRepository batchRepository,
            IRegisterNewBatch registerNewBatch,
            IModifyBatchStock modifyBatchStock
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
            var batches = await batchRepository.GetBatchesDeatiledAsync();

            return Ok(batches);
        }

        // GET api/batch/5
        [HttpGet("{batchId}")]
        public async Task<IActionResult> GetBatch([FromRoute] uint batchId) 
        {
            var batch = await batchRepository.GetBatchDeatiledAsync(batchId);

            if (batch == null)
            {
                return NotFound(batchId);
            }

            return Ok(batch);
        }

        // GET api/batch/history/5
        [HttpGet("history/{batchId}")]
        public async Task<IActionResult> GetBatchHistory([FromRoute] uint batchId)
        {
            var batch = await batchRepository.GetBatchWithHistoryAsync(batchId);

            if (batch == null)
            {
                return NotFound($"could not find batch {batchId}");
            }

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

            var RequestModel = new RegisterNewBatchRequest(
                                        newBatch.ProductId, 
                                        newBatch.Expiration, 
                                        newBatch.UnitAmount
                                        );

            try
            {
                var Response = await registerNewBatch.Async(RequestModel);
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
                //TODO: log input that caused an error
                throw;
            }
        }

        // PATCH api/batch/5
        [HttpPatch("{batchId}")]
        public async Task<IActionResult> ModifyBatchStock([FromRoute] uint batchId, [FromBody] BatchStockModification changeRequest)
        {
            var requestModel = new ModifyBatchStockRequest
            {
                BatchId = batchId,
                NewUnitAmount = changeRequest.NewUnitAmount,
                ReasonForChange = changeRequest.ReasonForChange
            };

            Batch modifiedBatch;

            try
            {
                modifiedBatch = await modifyBatchStock.ExecuteAsync(requestModel);
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
                throw;
            }
        }
    }
}
