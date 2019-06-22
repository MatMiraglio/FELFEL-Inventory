using FELFEL.Domain;
using System;
using System.Collections.Generic;

namespace FELFEL.UseCases.RegisterNewBatch
{
    public class RegisterNewBatch : IRegisterNewBatch
    {
        private readonly IUnitOfWork unitOfWork;

        public RegisterNewBatch(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Batch Execute(RegisterNewBatchRequest RequestModel)
        {
            if (RequestModel.Expiration < DateTime.Now)
            {
                throw new ArgumentException($"Cannot register a product that is already expired.");
            }

            var product = unitOfWork.Products.Get(RequestModel.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with Id {RequestModel.ProductId} does not exist.");
            }

            var batch = new Batch(product, RequestModel.Expiration, RequestModel.UnitAmount);

            unitOfWork.Batches.Add(batch);
            unitOfWork.Complete();

            return batch;
        }
    }
}
