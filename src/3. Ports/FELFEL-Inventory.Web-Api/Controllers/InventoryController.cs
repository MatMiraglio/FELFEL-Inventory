using System;
using System.Threading.Tasks;
using FELFEL.UseCases.GetFreshnessOverview;
using FELFEL.UseCases.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FELFEL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly Lazy<IBatchRepository> batchRepository;
        private readonly Lazy<IGetFreshnessOverview> getFreshnessOverview;

        public InventoryController(
            Lazy<IBatchRepository> batchRepository,
            Lazy<IGetFreshnessOverview> getFreshnessOverview
            )
        {
            this.batchRepository = batchRepository;
            this.getFreshnessOverview = getFreshnessOverview;
        }

        // GET api/inventory/5
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetInventoryPerProduct([FromRoute] int productId)
        {
            try
            {
                var batches = await batchRepository.Value.GetBatchesByProduct(productId);
                return Ok(batches);
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }

        }

        // GET api/inventory/freshness
        [Route("api/[controller]/freshness/overview")]
        [HttpGet("freshness/overview")]
        public async Task<IActionResult> GetInventoryFreshnessOverview()
        {
            try
            {
                var overview = await getFreshnessOverview.Value.ExecuteAsync();
                return Ok(overview);
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }
    }
}
