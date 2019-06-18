using System;
using System.Collections.Generic;

namespace FELFEL.Domain
{
    public class Batch
    {
        public int Id { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Arrival { get; set; }
        public string ProductName { get; set; }
        public int OriginalUnitAmount { get; set; }
        public int RemainingUnits { get; set; }

        public Batch() {}
    }
}
