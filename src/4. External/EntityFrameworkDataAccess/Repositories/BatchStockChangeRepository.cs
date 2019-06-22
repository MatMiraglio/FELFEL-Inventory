using FELFEL.Domain;
using FELFEL.UseCases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.External.EntityFrameworkDataAccess.Repositories
{
    class BatchStockChangeRepository : Repository<BatchStockChange>, IBatchStockChangeRepository
    {
        public BatchStockChangeRepository(DbContext context) : base(context)
        {
        }
    }
}
