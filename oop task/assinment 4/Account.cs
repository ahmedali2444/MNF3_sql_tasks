using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace practical
{
    internal class Account
    {
        public string AccountName { get; }
        public int AccountNumber { get; }
        public double Balance { get; private set; }
        public string Type { get; }
        public DateTime DateOpened { get; }

        private static int nextAccountNumber = 1;

        public double InterestRate { get; private set; }  
        public double OverdraftLimit { get; private set; } 

        public List<Transaction> Transactions { get; } = new List<Transaction>();

        public Account(string accountName, string type, double balance, double interestRate = 0, double overdraftLimit = 0)
        {
            DateOpened = DateTime.Now;
            AccountName = accountName;
            AccountNumber = nextAccountNumber++;
            Balance = balance;
            Type = type;
            InterestRate = interestRate;
            OverdraftLimit = overdraftLimit;

            Transactions.Add(new Transaction("Open", balance, "Opening balance"));
        }

        public void displayAccountInfo()
        {
            Console.WriteLine($"Account Name : {AccountName}\nAccount Balance : {Balance}");
        }

        public bool Deposit(double amount)
        {
            if (amount <= 0) return false;
            Balance += amount;
            Transactions.Add(new Transaction("Deposit", amount, "Cash deposit"));
            return true;
        }

        public bool Withdraw(double amount)
        {
            if (amount <= 0) return false;

            double available = Balance;
            if (Type.ToLower() == "current")
            {
                available = Balance + OverdraftLimit;
            }

            if (amount > available) return false;

            Balance -= amount;
            Transactions.Add(new Transaction("Withdraw", amount, "Cash withdrawal"));
            return true;
        }

        public static bool Transfer(Account from, Account to, double amount)
        {
            if (from == null || to == null) return false;
            if (amount <= 0) return false;

            double available = from.Balance;
            if (from.Type.ToLower() == "current")
            {
                available = from.Balance + from.OverdraftLimit;
            }
            if (amount > available) return false;

            from.Balance -= amount;
            to.Balance += amount;

            from.Transactions.Add(new Transaction("TransferOut", amount, "To Acc#" + to.AccountNumber));
            to.Transactions.Add(new Transaction("TransferIn", amount, "From Acc#" + from.AccountNumber));
            return true;
        }

        public double CalculateMonthlyInterest()
        {
            if (Type.ToLower() != "savings" || InterestRate <= 0) return 0;
            return Balance * (InterestRate / 100.0) / 12.0;
        }
    }
}
