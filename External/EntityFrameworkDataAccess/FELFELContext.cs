using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FELFEL.Domain;

namespace FELFEL.External.EntityFrameworkDataAccess
{
        public class FELFELContext : DbContext
        {
            public FELFELContext()
                : base()
            {
            }

            public virtual DbSet<Batch> Batches { get; set; }

        }
    
}
