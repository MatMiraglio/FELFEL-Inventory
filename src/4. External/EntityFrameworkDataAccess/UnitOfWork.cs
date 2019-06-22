using FELFEL.External.EntityFrameworkDataAccess;
using FELFEL.External.EntityFrameworkDataAccess.Repositories;
using FELFEL.UseCases;
using FELFEL.UseCases.Repositories;
using System.Threading.Tasks;

namespace FELFEL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FELFELContext _context;

        public UnitOfWork(FELFELContext context)
        {
            _context = context;
            Batches = new BatchRepository(_context);
            Products = new ProductRepository(_context);
            StockChanges = new BatchStockChangeRepository(_context);
        }

        public IBatchRepository Batches { get; private set; }
        public IProductRepository Products { get; private set; }
        public IBatchStockChangeRepository StockChanges { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}