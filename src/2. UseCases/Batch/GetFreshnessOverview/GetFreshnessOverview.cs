using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FELFEL.Domain;
using FELFEL.UseCases.Repositories;

namespace FELFEL.UseCases.GetFreshnessOverview
{
    public class GetFreshnessOverview : IGetFreshnessOverview

    {
        private readonly IBatchRepository batchRepository;

        public GetFreshnessOverview(IBatchRepository batchRepository)
        {
            this.batchRepository = batchRepository;
        }

        public async Task<FreshnessOverview> ExecuteAsync()
        {
            IEnumerable<Batch> batches = await batchRepository.GetBatchesDeatiledAsync();

            int expiredCount = batches.Where(batch => batch.IsExpired).Count();
            int freshCount = batches.Where(batch => batch.State == BatchState.fresh).Count();

            IEnumerable<Batch> expiringBatches = batches.Where(batch => batch.State == BatchState.expiring);
            IEnumerable<Batch> batchesExpiringToday = expiringBatches.Where(batch => batch.Expiration.Date == DateTime.Today.Date);

            IEnumerable<Product> productsExpiring = expiringBatches.Select(x => x.ProductType).Distinct();

            var freshnessOverview = new FreshnessOverview
            {
                AmountBatchesExpired = expiredCount,
                AmountBatchesFresh = freshCount,
                AmountBatchesExpiring = expiringBatches.Count(),
                BatchesExpiring = expiringBatches.ToList(),
                BatchesExpiringToday = batchesExpiringToday.ToList()

            };

            return freshnessOverview;
        }
    }
}
