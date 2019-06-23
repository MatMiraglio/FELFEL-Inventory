using FELFEL.Domain;
using System;

namespace FELFEL.UseCases.RegisterNewBatch
{
    public class RegisterNewBatchRequest
    {
        public RegisterNewBatchRequest(int productId, DateTime expiration, int unitAmount)
        {
            this.ProductId = productId;
            this.Expiration = expiration;
            this.UnitAmount = unitAmount;
        }

        public int ProductId { get; set; }
        public DateTime Expiration { get; set; }
        public int UnitAmount { get; set; }
    }
}
