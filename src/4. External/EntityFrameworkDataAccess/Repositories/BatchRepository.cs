using FELFEL.Domain;
using FELFEL.UseCases.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.External.EntityFrameworkDataAccess.Repositories
{
    class BatchRepository : Repository<Batch>, IBatchRepository
    {
        public BatchRepository(FELFELContext context)
            : base(context)
        {}

        public virtual DbSet<Batch> Authors { get; set; }
    }
}
