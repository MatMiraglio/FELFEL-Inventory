using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FELFEL.WebApi.ExternalModels
{
    public class NewBatch
    {
        public NewBatch() {}

        [Required]
        public Product ProductType { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public uint OriginalUnitAmount { get; set; }

    }
}
