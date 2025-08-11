using System;
using System.Collections.Generic;

public class AccountBase
{
    public string Id { get; set; }
    public string Owner { get; set; }
    public decimal Amount { get; set; }

    public AccountBase(string id, string owner, decimal amount)
    {
        Id = id;
        Owner = owner;
        Amount = amount;
    }

    public virtual decimal GetInterest()
    {
        return 0;
    }

    public virtual void PrintSummary()
    {
        Console.WriteLine($"Account ID: {Id}");
        Console.WriteLine($"Owner: {Owner}");
        Console.WriteLine($"Balance: {Amount:C}");
    }
}

public class SavingsAccount : AccountBase
{
    public decimal RatePercent { get; set; }

    public SavingsAccount(string id, string owner, decimal amount, decimal ratePercent)
        : base(id, owner, amount)
    {
        RatePercent = ratePercent;
    }

    public override decimal GetInterest()
    {
        return Amount * RatePercent / 100m;
    }

    public override void PrintSummary()
    {
        base.PrintSummary();
        Console.WriteLine($"Interest Rate: {RatePercent}%");
    }
}

public class CheckingAccount : AccountBase
{
    public decimal OverdraftCap { get; set; }

    public CheckingAccount(string id, string owner, decimal amount, decimal overdraftCap)
        : base(id, owner, amount)
    {
        OverdraftCap = overdraftCap;
    }

    public override decimal GetInterest()
    {
        return 0m; 
    }

    public override void PrintSummary()
    {
        base.PrintSummary();
        Console.WriteLine($"Overdraft Limit: {OverdraftCap:C}");
    }
}

class App
{
    static void Main()
    {
        var savingsAcc = new SavingsAccount("S001", "Ali Ahmed", 5000m, 5m);

        var checkingAcc = new CheckingAccount("C001", "Sara Mohamed", 3000m, 1000m);

        var portfolio = new List<AccountBase> { savingsAcc, checkingAcc };

        foreach (var acc in portfolio)
        {
            acc.PrintSummary();
            Console.WriteLine($"Calculated Interest: {acc.GetInterest():C}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
