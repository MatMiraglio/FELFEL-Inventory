using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.Domain
{
    public class BatchStockChange
    {
        public BatchStockChange(Batch batch, uint oldAmount, uint newAmount)
        {
            Batch = batch;
            OldAmount = oldAmount;
            NewAmount = newAmount;
            TimeOfChange = DateTime.Now;
        }

        public BatchStockChange() {}

        public int ID { get; set; }
        public Batch Batch { get; set; }
        public uint OldAmount { get; set; }
        public uint NewAmount { get; set; }
        public DateTime TimeOfChange { get; set; }
        public int AmountDifference
        {
            get
            {
                var diff = OldAmount - NewAmount;
                return checked((int) diff ); 
            }
        }

        public string Message { get; set; }

    }
}
