namespace FELFEL.UseCases.ModifyBatchStock
{
    public class ModifyBatchStockRequest
    {
        public uint BatchId { get; set; }
        public uint NewUnitAmount { get; set; }
        public string ReasonForChange { get; set; }
    }
}