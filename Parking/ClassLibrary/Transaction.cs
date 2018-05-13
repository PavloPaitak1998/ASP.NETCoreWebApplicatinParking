using System;

namespace ClassLibrary
{
    public class Transaction
    {
        public DateTime TransactionTime { get; set; }
        public int CarId { get; set; }
        public double Payment { get; set; }

        public override string ToString()
        {
            return "TransactionTime: " + TransactionTime + " Car Id: " + CarId + " Payment: " + Payment;
        }
    }
}
