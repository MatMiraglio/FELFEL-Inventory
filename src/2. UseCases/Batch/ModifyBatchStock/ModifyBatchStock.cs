using System;
using System.Collections.Generic;
using System.Text;
using FELFEL.Domain;

namespace FELFEL.UseCases.ModifyBatchStock
{
    public class ModifyBatchStock : IModifyBatchStock
    {
        private readonly IUnitOfWork unitOfWork;

        public ModifyBatchStock(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        } 

        public Batch Execute(ModifyBatchStockRequest RequestModel)
        {
            Validate(RequestModel);

            var batch = unitOfWork.Batches.SingleOrDefault(x => x.Id == RequestModel.BatchId);

            if (batch == null)
            {
                throw new KeyNotFoundException($"Batch with id {RequestModel.BatchId} not found");
            }

            var StockChange = new BatchStockChange(batch, batch.RemainingUnits, RequestModel.NewUnitAmount);

            unitOfWork.StockChanges.Add(StockChange);

            batch.RemainingUnits = RequestModel.NewUnitAmount;
            batch.History.Add(StockChange);

            unitOfWork.Complete();

            return batch;
        }

        private void Validate(ModifyBatchStockRequest RequestModel)
        {
            if (string.IsNullOrWhiteSpace(RequestModel.ReasonForChange))
            {
                throw new ArgumentException("Must provide a reasong for modifying the stock");
            }

            if (RequestModel.NewUnitAmount < 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }
        }
    }
}
