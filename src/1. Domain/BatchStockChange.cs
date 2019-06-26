using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.Domain
{
    public class BatchStockChange
    {
        public BatchStockChange(int oldAmount, int newAmount, string message)
        {
            OldAmount = oldAmount;
            NewAmount = newAmount;
            TimeOfChange = DateTime.Now;
            Message = message;
        }

        public BatchStockChange() {}

        public int ID { get; set; }
        public int OldAmount { get; set; }
        public int NewAmount { get; set; }
        public DateTime TimeOfChange { get; set; }
        public string Message { get; set; }

        public int AmountDifference
        {
            get
            {
                return NewAmount - OldAmount;
            }
        }


    }
}
