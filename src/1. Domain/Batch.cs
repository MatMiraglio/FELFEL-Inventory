﻿using System;
using System.Collections.Generic;

namespace FELFEL.Domain
{
    public class Batch
    {
        public Batch()
        {
            History = new HashSet<BatchStockChange>();
        }

        public Batch(Product product, DateTime expiration, int unitAmount)
        {
            ProductType = product;
            Expiration = expiration;
            Arrival = DateTime.Now;
            OriginalUnitAmount = unitAmount;
            RemainingUnits = unitAmount;
            History = new HashSet<BatchStockChange>();
        }

        public int Id { get; set; }
        public Product ProductType { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Arrival { get; set; }
        public int OriginalUnitAmount { get; set; }
        public int RemainingUnits { get; set; }
        public BatchState State
        {
            get
            {
                if (IsExpired)
                {
                    return BatchState.expired;
                }
                if (ExpirationIsInLessThanDays(14))
                {
                    return BatchState.expiring;
                }

                return BatchState.fresh;
            }
        }

        public bool IsExpired
        {
            get
            {
                return Expiration < DateTime.Now;
            }
        }

        public virtual ICollection<BatchStockChange> History { get; set; }


        public bool ExpirationIsInLessThanDays(int daysInTheFuture)
        {
            return Expiration < DateTime.Now.AddDays(daysInTheFuture);
        }
    }

}
