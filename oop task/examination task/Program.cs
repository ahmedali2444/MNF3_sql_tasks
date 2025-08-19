using System;
using System.Collections.Generic;

namespace BankingDemo
{
    public class BankAccount
    {
        public string AccountNumber { get; }
        public string HolderName { get; }
        public decimal Balance { get; protected set; }

        public BankAccount(string accountNumber, string holderName, decimal openingBalance = 0m)
        {
            AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
            HolderName = holderName ?? throw new ArgumentNullException(nameof(holderName));
            Balance = openingBalance;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }

        public virtual void ShowAccountDetails()
        {
            Console.WriteLine($"[{GetType().Name}] {AccountNumber} | Holder: {HolderName} | Balance: {Balance:C}");
        }

        public virtual decimal CalculateInterest(int months)
        {
            if (months < 0) throw new ArgumentOutOfRangeException(nameof(months));
            return 0m;
        }
    }

    public class SavingAccount : BankAccount
    {
        public decimal InterestRate { get; }

        public SavingAccount(string accountNumber, string holderName, decimal openingBalance, decimal interestRate)
            : base(accountNumber, holderName, openingBalance)
        {
            if (interestRate < 0m) throw new ArgumentOutOfRangeException(nameof(interestRate));
            InterestRate = interestRate;
        }

        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"    Interest Rate: {InterestRate:P}");
        }

        public override decimal CalculateInterest(int months)
        {
            if (months < 0) throw new ArgumentOutOfRangeException(nameof(months));
            decimal years = months / 12m;
            return Balance * InterestRate * years;
        }
    }

    public class CurrentAccount : BankAccount
    {
        public decimal OverdraftLimit { get; }

        public CurrentAccount(string accountNumber, string holderName, decimal openingBalance, decimal overdraftLimit)
            : base(accountNumber, holderName, openingBalance)
        {
            if (overdraftLimit < 0m) throw new ArgumentOutOfRangeException(nameof(overdraftLimit));
            OverdraftLimit = overdraftLimit;
        }

        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"    Overdraft Limit: {OverdraftLimit:C}");
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (Balance - amount < -OverdraftLimit)
                throw new InvalidOperationException("Withdrawal would exceed overdraft limit.");
            Balance -= amount;
        }

        public override decimal CalculateInterest(int months) => 0m;
    }

    public class Program
    {
        public static void Main()
        {
            var saving = new SavingAccount("SA-001", "Abdullah", 10000m, 0.08m);
            var current = new CurrentAccount("CA-001", "Tech Co. Ops", 2000m, 1500m);

            var accounts = new List<BankAccount> { saving, current };

            foreach (var acc in accounts)
            {
                acc.ShowAccountDetails();
                var interest12m = acc.CalculateInterest(12);
                Console.WriteLine($"    Interest for 12 months: {interest12m:C}");
                Console.WriteLine();
            }

            Console.WriteLine("Attempting a large withdrawal from CurrentAccount...");
            current.Withdraw(3200m);
            current.ShowAccountDetails();
        }
    }
}
