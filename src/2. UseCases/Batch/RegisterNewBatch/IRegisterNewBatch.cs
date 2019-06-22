using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FELFEL.UseCases.RegisterNewBatch
{
    public interface IRegisterNewBatch
    {
        Task<Batch> Async(RegisterNewBatchRequest RequestModel);
    }
}
