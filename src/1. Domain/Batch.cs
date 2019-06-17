using System;
using System.Collections.Generic;

namespace FELFEL.Domain
{
    public class Batch
    {
        public int ID { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Arrival { get; set; }
        public List<Product> Products { get; set; }
        public string ProductName { get; set; }
        public int OriginalUnitAmount { get; set; }
        public int RemainingUnits { get; set; }

        public Batch()
	    {
            Products = new List<Product>();
	    }
    }
}
