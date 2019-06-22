using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FELFEL.UseCases.Repositories
{
    public interface IBatchRepository : IRepository<Batch>
    {
        IEnumerable<Batch> GetInventoryPerProduct(int productId);
        Task<IEnumerable<Batch>> GetBatchesDeatiled();
        Task<Batch> GetBatchDeatiled(uint batchId);
        Task<Batch> GetBatchWithHistory(uint batchId);
    }
}
