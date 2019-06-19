using FELFEL.Domain;
using Microsoft.EntityFrameworkCore;

namespace FELFEL.External.EntityFrameworkDataAccess
{
    public class FELFELContext : DbContext
    {
        public FELFELContext()
            : base()
        {
        }

        public virtual DbSet<Batch> Authors { get; set; }
    }
}
