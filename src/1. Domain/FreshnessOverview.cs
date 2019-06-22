using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.Domain
{
    public class FreshnessOverview
    {
        public int AmountBatchesExpired { get; set; }
        public int AmountBatchesFresh { get; set; }
        public int AmountBatchesExpiring { get; set; }
        public ICollection<Batch> BatchesExpiring { get; set; }
        public ICollection<Batch> BatchesExpiringToday { get; set; }
        public Product[] ProductsSoonToExpire { get; set; }
    }
}
