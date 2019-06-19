using FELFEL.Domain;
using Microsoft.EntityFrameworkCore;

namespace FELFEL.External.EntityFrameworkDataAccess
{
    public class FELFELContext : DbContext
    {
        public FELFELContext(DbContextOptions options) : base(options) {}

        public virtual DbSet<Batch> Batches { get; set; }
    }
}
