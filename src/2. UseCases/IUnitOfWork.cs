using FELFEL.UseCases.Repositories;
using System.Threading.Tasks;

namespace FELFEL.UseCases
{
    public interface IUnitOfWork
    {
        IBatchRepository Batches { get; }
        IProductRepository Products { get; }
        IBatchStockChangeRepository StockChanges { get; }
        Task<int> CompleteAsync();
    }
}
