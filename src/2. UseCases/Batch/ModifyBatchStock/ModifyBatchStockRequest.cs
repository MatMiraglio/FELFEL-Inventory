namespace FELFEL.UseCases.ModifyBatchStock
{
    public class ModifyBatchStockRequest
    {
        public ModifyBatchStockRequest(int batchId, int newUnitAmount, string reasonForChange)
        {
            BatchId = batchId;
            NewUnitAmount = newUnitAmount;
            ReasonForChange = reasonForChange;
        }

        public int BatchId { get; set; }
        public int NewUnitAmount { get; set; }
        public string ReasonForChange { get; set; }
    }
}