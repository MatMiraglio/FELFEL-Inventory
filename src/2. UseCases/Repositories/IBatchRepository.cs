using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FELFEL.UseCases.Repositories
{
    public interface IBatchRepository : IRepository<Batch>
    {
        Task<IEnumerable<Batch>> GetBatchesByProduct(int productId);
        Task<IEnumerable<Batch>> GetBatchesDeatiledAsync();
        Task<Batch> GetBatchDeatiledAsync(uint batchId);
        Task<Batch> GetBatchWithHistoryAsync(uint batchId);
    }
}
