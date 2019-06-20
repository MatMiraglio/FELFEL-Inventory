using FELFEL.Domain;
using System;

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
            var batch = new Batch()
            {
                ProductType = RequestModel.ProductType,
                Expiration = RequestModel.Expiration,
                Arrival = DateTime.Now,
                OriginalUnitAmount = RequestModel.OriginalUnitAmount,
                RemainingUnits = RequestModel.OriginalUnitAmount
            };
            if (unitOfWork.Products.Get(RequestModel.ProductType.Id) == null)
            {
                unitOfWork.Products.Add(RequestModel.ProductType);
            }

            unitOfWork.Batches.Add(batch);
            unitOfWork.Complete();

            return batch;
        }
    }
}
