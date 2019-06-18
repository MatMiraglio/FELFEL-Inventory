using FELFEL.UseCases.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.UseCases
{
    public interface IUnitOfWork
    {
        IBatchRepository Batches { get; }
        int Complete();
    }
}
