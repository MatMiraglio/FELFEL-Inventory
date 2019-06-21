using System;
using System.Collections.Generic;
using System.Text;
using FELFEL.Domain;

namespace FELFEL.UseCases.ModifyBatchRemainingUnits
{
    public class ModifyBatchRemainingUnits : IModifyBatchRemainingUnits
    {
        private readonly IUnitOfWork unitOfWork;

        public ModifyBatchRemainingUnits(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        } 

        public Batch Execute(ModifyBatchRemainingUnitsRequest RequestModel)
        {
            var batch = unitOfWork.Batches.SingleOrDefault(x => x.Id == RequestModel.BatchId);

            if (batch == null)
            {
                throw new ArgumentException("");
            }

            return new Batch();
        }
    }
}
