using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FELFEL.Domain;
using FELFEL.External.EntityFrameworkDataAccess;
using FELFEL.Persistence;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.WebApi.ExternalModels;
using Microsoft.AspNetCore.Mvc;

namespace FELFEL_Inventory.Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IRegisterNewBatch registerNewBatchCommand;

        public BatchController(IRegisterNewBatch registerNewBatchCommand)
        {
            this.registerNewBatchCommand = registerNewBatchCommand;
        }


        // GET api/batch
        [HttpGet]
        public ActionResult<IEnumerable<Batch>> Get()
        {
            return 
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
                var ResponseModel = registerNewBatchCommand.Execute(RequestModel);
                return Ok(ResponseModel);
            }
            catch (Exception)
            {
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
        }
    }
}
