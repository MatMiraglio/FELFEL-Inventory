using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.UseCases.RegisterNewBatch
{
    public interface IRegisterNewBatch
    {
        Batch Execute(RegisterNewBatchRequest RequestModel);
    }
}
