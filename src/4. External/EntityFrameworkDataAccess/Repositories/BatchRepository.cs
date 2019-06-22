using FELFEL.Domain;
using FELFEL.UseCases.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FELFEL.External.EntityFrameworkDataAccess.Repositories
{
    public class BatchRepository : Repository<Batch>, IBatchRepository
    {
        public BatchRepository(FELFELContext context) : base(context)
        {

        }

        public IEnumerable<Batch> GetInventoryPerProduct(int productId)
        {
            return Find(batch => batch.ProductType.Id == productId);
        }

        public async Task<IEnumerable<Batch>> GetBatchesDeatiled()
        {
             return await _entities
                .Include(x => x.ProductType)
                .Include(x => x.History)
                .ToListAsync();
        }

        public async Task<Batch> GetBatchDeatiled(uint batchId)
        {
            return await _entities
               .Include(x => x.ProductType)
               .SingleOrDefaultAsync(batch => batch.Id == batchId);
        }

        public async Task<Batch> GetBatchWithHistory(uint batchId)
        {
            return await _entities
               .Include(x => x.ProductType)
               .Include(x => x.History)
               .SingleOrDefaultAsync(batch => batch.Id == batchId);
        }
    }
}
