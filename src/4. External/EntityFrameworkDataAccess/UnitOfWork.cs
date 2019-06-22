using FELFEL.External.EntityFrameworkDataAccess;
using FELFEL.External.EntityFrameworkDataAccess.Repositories;
using FELFEL.UseCases;
using FELFEL.UseCases.Repositories;

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

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}