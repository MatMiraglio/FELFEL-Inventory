using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FELFEL.WebApi.InputModels
{
    public class ChangeBatchUnits
    {
        public uint NewUnitAmount { get; set; }
        public string ReasonForChange { get; set; }
    }
}
