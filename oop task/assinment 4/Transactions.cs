using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practical
{
    internal class Transaction
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }

        public Transaction(string type, double amount, string note)
        {
            Date = DateTime.Now;
            Type = type;
            Amount = amount;
            Note = note;
        }
    }
}
