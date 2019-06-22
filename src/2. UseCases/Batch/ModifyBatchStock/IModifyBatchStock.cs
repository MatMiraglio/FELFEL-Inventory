using FELFEL.Domain;
using System.Threading.Tasks;

namespace FELFEL.UseCases.ModifyBatchStock
{
    public interface IModifyBatchStock
    {
        Task<Batch> ExecuteAsync(ModifyBatchStockRequest RequestModel);
    }
}