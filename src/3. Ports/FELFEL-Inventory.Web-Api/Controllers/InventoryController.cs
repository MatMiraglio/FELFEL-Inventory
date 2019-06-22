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
        private readonly IBatchRepository batchRepository;
        private readonly IGetFreshnessOverview getFreshnessOverview;

        public InventoryController(
            IBatchRepository batchRepository,
            IGetFreshnessOverview getFreshnessOverview
            )
        {
            this.batchRepository = batchRepository;
            this.getFreshnessOverview = getFreshnessOverview;
        }

        // GET api/inventory/5
        [HttpGet("{productId}")]
        public IActionResult GetInventoryPerProduct(int productId)
        {
            var batches = batchRepository.GetInventoryPerProduct(productId);

            return Ok(batches);
        }

        // GET api/inventory/freshness
        [HttpGet("/freshness")]
        public async Task<IActionResult> GetInventoryFreshnessOverview()
        {
            var overview = await getFreshnessOverview.ExecuteAsync();

            return Ok(overview);
        }
    }
}
