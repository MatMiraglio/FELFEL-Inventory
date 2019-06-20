using System;
using System.Collections.Generic;
using FELFEL.Domain;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.Repositories;
using FELFEL.WebApi.ExternalModels;
using Microsoft.AspNetCore.Mvc;

namespace FELFEL_Inventory.Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IRegisterNewBatch registerNewBatchCommand;
        private readonly IBatchRepository batchRepository;

        public BatchController(
            IRegisterNewBatch registerNewBatchCommand,
            IBatchRepository batchRepository
            )
        {
            this.registerNewBatchCommand = registerNewBatchCommand;
            this.batchRepository = batchRepository;
        }


        // GET api/batch
        [HttpGet]
        public ActionResult<IEnumerable<Batch>> Get()
        {
            var batches = batchRepository.GetAll();

            return Ok(batches);
        }

        // GET api/batch/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int batchId)
        {
            return "value";
        }

        // POST api/batch
        [HttpPost]
        public IActionResult Post([FromBody] NewBatch newBatch)
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
                ProductType = newBatch.ProductType,
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

        // PUT api/batch/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/batch/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var allBatches = batchRepository.GetAll();
            batchRepository.RemoveRange(allBatches);

            batchRepository.Find(batch => batch.ProductType.Id == 1);
        }
    }
}
