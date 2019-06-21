using FELFEL.Domain;
using FELFEL.UseCases.ModifyBatchRemainingUnits;

namespace FELFEL.UseCases.ModifyBatchRemainingUnits
{
    public interface IModifyBatchRemainingUnits
    {
        Batch Execute(ModifyBatchRemainingUnitsRequest RequestModel);
    }
}