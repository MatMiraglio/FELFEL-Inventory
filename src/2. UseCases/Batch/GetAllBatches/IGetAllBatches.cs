using System;
using System.Collections.Generic;
using FELFEL.Domain;


namespace FELFEL.UseCases.GetAllBatches
{
    public interface IGetAllBatches
    {
        IEnumerable<Batch> Execute();
    }
}
