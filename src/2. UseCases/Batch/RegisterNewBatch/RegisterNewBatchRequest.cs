using FELFEL.Domain;
using System;

namespace FELFEL.UseCases.RegisterNewBatch
{
    public class RegisterNewBatchRequest
    {
        public Product ProductType { get; set; }
        public DateTime Expiration { get; set; }
        public uint OriginalUnitAmount { get; set; }
    }
}
