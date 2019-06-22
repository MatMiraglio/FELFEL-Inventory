using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FELFEL.UseCases.RegisterNewBatch
{
    public class RegisterNewBatch : IRegisterNewBatch
    {
        private readonly IUnitOfWork unitOfWork;

        public event EventHandler<BatchEventArgs> BatchRegistered;

        public RegisterNewBatch(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Batch> ExecuteAsync(RegisterNewBatchRequest RequestModel)
        {
            if (RequestModel.Expiration < DateTime.Now)
            {
                throw new ArgumentException($"Cannot register a product that is already expired.");
            }

            var product = await unitOfWork.Products.GetAsync(RequestModel.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with Id {RequestModel.ProductId} does not exist.");
            }

            var batch = new Batch(product, RequestModel.Expiration, RequestModel.UnitAmount);

            unitOfWork.Batches.Add(batch);
            await unitOfWork.CompleteAsync();

            OnBatchRegistered(batch);

            return batch;
        }

        protected virtual void OnBatchRegistered(Batch batch)
        {
            BatchRegistered?.Invoke(this, new BatchEventArgs() { Batch = batch });
        }
    }

    public class BatchEventArgs : EventArgs
    {
        public Batch Batch { get; set; }
    }
}
