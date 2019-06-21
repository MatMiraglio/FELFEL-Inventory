using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FELFEL.Domain;
using FELFEL.UseCases.ModifyBatchRemainingUnits;
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
        private readonly IModifyBatchRemainingUnits modifyBatchUnitsCommand;
        private readonly IBatchRepository batchRepository;

        public BatchController(
            IRegisterNewBatch registerNewBatchCommand,
            IModifyBatchRemainingUnits modifyNewBatchCommand,
            IBatchRepository batchRepository
            )
        {
            this.registerNewBatchCommand = registerNewBatchCommand;
            this.modifyBatchUnitsCommand = modifyNewBatchCommand;
            this.batchRepository = batchRepository;
        }


        // GET api/batch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batch>>> Get()
        {
            var batches = await batchRepository.GetBatchesDeatiled();

            return Ok(batches);
        }

        // GET api/batch/5
        [HttpGet("{batchId}")]
        public ActionResult<Batch> Get([FromRoute] int batchId)
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

            if (newBatch.Expiration < DateTime.Now)
            {
                return BadRequest(new { message = "Expiration date cannot be in the past" });
            }


            //After setting up a DI container when can use a framework like automapper for this kind of work
            var RequestModel = new RegisterNewBatchRequest()
            {
                ProductId = newBatch.ProductId,
                Expiration = newBatch.Expiration,
                OriginalUnitAmount = newBatch.OriginalUnitAmount
            };

            try
            {
                var Response = registerNewBatchCommand.Execute(RequestModel);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                //TODO: log input that caused an error
                throw;
            }
        }

        // PATCH api/batch/5
        [HttpPatch("{batchId}")]
        public IActionResult ChangeBatchRemainingUnits([FromRoute] uint batchId, [FromBody] ChangeBatchUnits changeRequest)
        {
            var requestModel = new ModifyBatchRemainingUnitsRequest
            {
                BatchId = batchId,
                NewUnitAmount = changeRequest.NewUnitAmount,
                ReasonForChange = changeRequest.ReasonForChange
            };

            var modifiedBatch = modifyBatchUnitsCommand.Execute(requestModel);

            return Ok(modifiedBatch);
        }
    }
}
