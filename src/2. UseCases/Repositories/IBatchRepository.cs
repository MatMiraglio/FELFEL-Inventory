using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FELFEL.UseCases.Repositories
{
    public interface IBatchRepository : IRepository<Batch>
    {
        Task<IEnumerable<Batch>> GetBatchesByProduct(int productId);
        Task<IEnumerable<Batch>> GetBatchesDeatiledAsync();
        Task<int> GetStock(int batchId);
        Task<IEnumerable<object>> GetAllStock();
        Task<Batch> GetBatchAsync(uint batchId);
        Task<Batch> GetBatchWithHistoryAsync(uint batchId);
        int GetCount(Expression<Func<Batch, bool>> predicate);
    }
}
