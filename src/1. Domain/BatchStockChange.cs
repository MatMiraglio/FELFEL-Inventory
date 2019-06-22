using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.Domain
{
    public class BatchStockChange
    {
        public BatchStockChange(Batch batch, uint newAmount, string message)
        {
            OldAmount = batch.RemainingUnits;
            NewAmount = newAmount;
            TimeOfChange = DateTime.Now;
            Message = message;
        }

        public BatchStockChange() {}

        public int ID { get; set; }
        public uint OldAmount { get; set; }
        public uint NewAmount { get; set; }
        public DateTime TimeOfChange { get; set; }
        public string Message { get; set; }

        public int AmountDifference
        {
            get
            {
                var diff = OldAmount - NewAmount;
                return checked((int) diff ); 
            }
        }


    }
}
