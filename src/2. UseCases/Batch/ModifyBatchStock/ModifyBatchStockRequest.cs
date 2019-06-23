namespace FELFEL.UseCases.ModifyBatchStock
{
    public class ModifyBatchStockRequest
    {
        public ModifyBatchStockRequest(uint batchId, uint newUnitAmount, string reasonForChange)
        {
            BatchId = batchId;
            NewUnitAmount = newUnitAmount;
            ReasonForChange = reasonForChange;
        }

        public uint BatchId { get; set; }
        public uint NewUnitAmount { get; set; }
        public string ReasonForChange { get; set; }
    }
}