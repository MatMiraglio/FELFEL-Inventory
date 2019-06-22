using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FELFEL.Domain;
using FELFEL.UseCases.ModifyBatchStock;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.Repositories;
using FELFEL.WebApi.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace FELFEL_Inventory.Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IRegisterNewBatch registerNewBatchCommand;
        private readonly IModifyBatchStock modifyBatchStock;
        private readonly IBatchRepository batchRepository;

        public BatchController(
            IRegisterNewBatch registerNewBatchCommand,
            IModifyBatchStock modifyNewBatchCommand,
            IBatchRepository batchRepository
            )
        {
            this.registerNewBatchCommand = registerNewBatchCommand;
            this.modifyBatchStock = modifyNewBatchCommand;
            this.batchRepository = batchRepository;
        }


        // GET api/batch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batch>>> GetAllBatch()
        {
            var batches = await batchRepository.GetBatchesDeatiled();

            return Ok(batches);
        }

        // GET api/batch/5
        [HttpGet("{batchId}")]
        public ActionResult<Batch> GetBatch([FromRoute] int batchId) 
        {
            var batch = batchRepository.Get(batchId);

            if (batch == null)
            {
                return NotFound(batchId);
            }

            return Ok(batch);
        }

        // GET api/batch/history/5
        [HttpGet("history/{batchId}")]
        public ActionResult<Batch> GetBatchHistory([FromRoute] int batchId)
        {
            var batch = batchRepository.Get(batchId);

            if (batch == null)
            {
                return NotFound(batchId);
            }

            return Ok(batch);
        }

        // POST api/batch
        [HttpPost]
        public IActionResult RegisterNewBatch([FromBody] NewBatch newBatch)
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
                var Response = registerNewBatchCommand.Execute(RequestModel);
                return Ok(Response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
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
        public IActionResult ModifyBatchStock([FromRoute] uint batchId, [FromBody] BatchStockModification changeRequest)
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
                modifiedBatch = modifyBatchStock.Execute(requestModel);
                return Ok(modifiedBatch);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new {ex.Message});
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
