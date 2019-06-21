using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public InventoryController(
            IBatchRepository batchRepository
            )
        {
            this.batchRepository = batchRepository;
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
        public IActionResult GetInventoryFreshnessOverview()
        {
            var batches = batchRepository.GetAll();

            return Ok(batches);
        }
    }
}
