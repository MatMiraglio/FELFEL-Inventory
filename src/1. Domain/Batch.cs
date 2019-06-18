using FELFEL_Inventory.Domain;
using System;
using System.Collections.Generic;

namespace FELFEL.Domain
{
    public class Batch
    {
        public Batch()
        {
            History = new HashSet<BatchChange>();
        }

        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Arrival { get; set; }
        public uint OriginalUnitAmount { get; set; }
        public uint RemainingUnits { get; set; }
        public BatchState State
        {
            get { return BatchState.fresh; }
        }
        public virtual ICollection<BatchChange> History { get; set; }

    }

}
