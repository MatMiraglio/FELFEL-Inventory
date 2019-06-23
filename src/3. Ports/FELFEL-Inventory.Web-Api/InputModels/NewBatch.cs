using FELFEL.UseCases.RegisterNewBatch;
using System;
using System.ComponentModel.DataAnnotations;

namespace FELFEL.WebApi.InputModels
{
    public class NewBatch
    {
        public NewBatch() {}

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public int UnitAmount { get; set; }

        public RegisterNewBatchRequest AsRegisterNewBatchRequest()
        {
            return new RegisterNewBatchRequest(ProductId, Expiration, UnitAmount);
        } 

    }
}
