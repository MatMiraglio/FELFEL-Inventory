using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public uint OriginalUnitAmount { get; set; }

    }
}
