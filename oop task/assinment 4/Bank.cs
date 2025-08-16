using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practical
{
    internal class Bank
    {
        public string Name { get; set; }
        public string BranchCode { get; set; }

        public List<Customer> Customers { get; } = new List<Customer>();

        private int nextCustomerId = 1;

        public Bank(string name, string branch_code)
        {
            Name = name;
            BranchCode = branch_code;
        }

        public void display()
        {
            Console.WriteLine("Bank name : " + Name + " | Branch Code : " + BranchCode);
        }

        private bool IsAllDigits(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i])) return false;
            }
            return true;
        }

        public Customer AddCustomer(string fullName, string nationalId, DateTime dob)
        {
            fullName = (fullName ?? "").Trim();
            nationalId = (nationalId ?? "").Trim();

            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3)
            {
                Console.WriteLine("Error: Full name required (at least 3 characters).");
                return null;
            }

            if (nationalId.Length != 14 || !IsAllDigits(nationalId))
            {
                Console.WriteLine("nat id should have 14 digit");
                return null;
            }

            foreach (var existing in Customers)
            {
                if (existing.NationalID == nationalId)
                {
                    Console.WriteLine("\r\nError: The national number is already registered.");
                    return null;
                }
            }

            if (dob.Date >= DateTime.Today)
            {
                Console.WriteLine("Error: Date of birth must be in the past.");
                return null;
            }
            if (dob.Date < DateTime.Today.AddYears(-120))
            {
                Console.WriteLine("Error: Date of birth does not make sense.");
                return null;
            }

            var c = new Customer(nextCustomerId++, fullName, nationalId, dob);
            Customers.Add(c);
            Console.WriteLine(
"Customer added successfully. ID = " + c.Id);
            return c;
        }

        public Customer FindCustomerById(int id)
        {
            foreach (var c in Customers)
            {
                if (c.Id == id) return c;
            }
            return null;
        }

        public List<Customer> SearchCustomers(string query)
        {
            query = query.ToLower();
            var list = new List<Customer>();
            foreach (var c in Customers)
            {
                if ((c.FullName != null && c.FullName.ToLower().Contains(query)) ||
                    (c.NationalID != null && c.NationalID.ToLower().Contains(query)))
                {
                    list.Add(c);
                }
            }
            return list;
        }

        public bool RemoveCustomer(int id)
        {
            var c = FindCustomerById(id);
            if (c == null) return false;
            if (!c.canBeRemove()) return false;

            Customers.Remove(c);
            return true;
        }

        public Account AddAccountToCustomer(int customerId, string accountName, string type, double openingBalance, double interestRate, double overdraftLimit)
        {
            var c = FindCustomerById(customerId);
            if (c == null) return null;

            var acc = new Account(accountName, type, openingBalance, interestRate, overdraftLimit);
            c.Accounts.Add(acc);
            return acc;
        }

        public Account FindAccountByNumber(int accountNumber)
        {
            foreach (var c in Customers)
            {
                foreach (var a in c.Accounts)
                {
                    if (a.AccountNumber == accountNumber) return a;
                }
            }
            return null;
        }

        public bool Deposit(int accountNumber, double amount)
        {
            var acc = FindAccountByNumber(accountNumber);
            if (acc == null) return false;
            return acc.Deposit(amount);
        }

        public bool Withdraw(int accountNumber, double amount)
        {
            var acc = FindAccountByNumber(accountNumber);
            if (acc == null) return false;
            return acc.Withdraw(amount);
        }

        public bool Transfer(int fromAccount, int toAccount, double amount)
        {
            var from = FindAccountByNumber(fromAccount);
            var to = FindAccountByNumber(toAccount);
            if (from == null || to == null) return false;
            return Account.Transfer(from, to, amount);
        }

        public double GetCustomerTotalBalance(int customerId)
        {
            var c = FindCustomerById(customerId);
            if (c == null) return 0;
            double total = 0;
            foreach (var a in c.Accounts) total += a.Balance;
            return total;
        }

        public void PrintBankReport()
        {
            Console.WriteLine("===== Bank Report =====");
            Console.WriteLine("Bank: " + Name + " | Branch: " + BranchCode);
            Console.WriteLine("Customers: " + Customers.Count);
            Console.WriteLine("-----------------------");

            foreach (var c in Customers)
            {
                Console.WriteLine("Customer ID: " + c.Id + " | Name: " + c.FullName + " | NationalID: " + c.NationalID);
                Console.WriteLine("Accounts: " + c.Accounts.Count);
                foreach (var a in c.Accounts)
                {
                    Console.WriteLine("AccountNumber: " + a.AccountNumber +
                                      " AccountName: " + a.AccountName +
                                      " Type: " + a.Type +
                                      " Balance: " + a.Balance.ToString("0.00") +
                                      " Opened: " + a.DateOpened.ToShortDateString());
                }
                Console.WriteLine("Total Balance: " + GetCustomerTotalBalance(c.Id).ToString("0.00"));
                Console.WriteLine("-----------------------");
            }
        }

        public void PrintAccountTransactions(int accountNumber)
        {
            var acc = FindAccountByNumber(accountNumber);
            if (acc == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine("=== Transactions for Account " + acc.AccountNumber + " (" + acc.AccountName + ")");
            foreach (var t in acc.Transactions)
            {
                Console.WriteLine(t.Date.ToString("yyyy-MM-dd HH:mm") + " " + t.Type + " " + t.Amount.ToString("0.00") + " " + t.Note);
            }
            Console.WriteLine("Current Balance: " + acc.Balance.ToString("0.00"));
        }
    }
}
