using FELFEL.Domain;
using System;
using System.Collections.Generic;

namespace FELFEL.UseCases.GetAllBatches
{
    public class GetAllBatches : IGetAllBatches
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBatches(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Batch> Execute()
        {
            return _unitOfWork.Batches.GetAll();
        }
    }
}
