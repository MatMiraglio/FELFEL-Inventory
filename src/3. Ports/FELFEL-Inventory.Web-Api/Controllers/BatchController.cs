using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FELFEL_Inventory.Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        // GET api/batch
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/batch/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int batchId)
        {
            return "value";
        }

        // POST api/batch
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
