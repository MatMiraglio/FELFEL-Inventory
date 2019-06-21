namespace FELFEL.UseCases.ModifyBatchRemainingUnits
{
    public class ModifyBatchRemainingUnitsRequest
    {
        public uint BatchId { get; set; }
        public uint NewUnitAmount { get; set; }
        public string ReasonForChange { get; set; }
    }
}