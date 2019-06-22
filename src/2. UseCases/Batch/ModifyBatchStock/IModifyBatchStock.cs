using FELFEL.Domain;
using FELFEL.UseCases.ModifyBatchStock;

namespace FELFEL.UseCases.ModifyBatchStock
{
    public interface IModifyBatchStock
    {
        Batch Execute(ModifyBatchStockRequest RequestModel);
    }
}