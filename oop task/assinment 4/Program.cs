using practical;
using System.Globalization;

namespace BankSystem
{
   
    public static class Program
    {
        public static void Main()
        {
            Console.Write("Enter Bank Name: ");
            string bankName = Console.ReadLine();

            Console.Write("Enter Branch Code: ");
            string branchCode = Console.ReadLine();

            Bank bank = new Bank(bankName, branchCode);
            Console.WriteLine("\nBank created successfully.");
            bank.display();

            while (true)
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("1) Add Customer");
                Console.WriteLine("2) Update Customer");
                Console.WriteLine("3) Remove Customer");
                Console.WriteLine("4) Search Customers");
                Console.WriteLine("5) Add Account to Customer");
                Console.WriteLine("6) Deposit");
                Console.WriteLine("7) Withdraw");
                Console.WriteLine("8) Transfer");
                Console.WriteLine("9) Show Customer Total Balance");
                Console.WriteLine("10) Calculate Monthly Interest (Savings)");
                Console.WriteLine("11) Bank Report");
                Console.WriteLine("12) Show Account Transactions");
                Console.WriteLine("0) Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        {
                            Console.Write("Full Name: ");
                            string name = Console.ReadLine();

                            Console.Write("National ID: ");
                            string nid = Console.ReadLine();

                            Console.Write("Date of Birth (yyyy-MM-dd): ");
                            string dobText = Console.ReadLine();

                            DateTime dob;
                            string[] formats = { "yyyy-MM-dd", "yyyy-M-d", "yyyy-MM-d", "yyyy-M-dd" };
                            if (!DateTime.TryParseExact(dobText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                            {
                                Console.WriteLine("invalid format , correct formate: yyyy-MM-dd  2004-06-09");
                                break;
                            }

                            var c = bank.AddCustomer(name, nid, dob);

                            if (c == null)
                            {
                                Console.WriteLine("error in inputs");
                                break;
                            }

                            Console.WriteLine("Customer added. ID = " + c.Id);
                            break;
                        }
                    case "2":
                        {
                            Console.Write("Customer ID: ");
                            int id;
                            if (!int.TryParse(Console.ReadLine(), out id)) { Console.WriteLine("Invalid ID."); break; }
                            var c = bank.FindCustomerById(id);
                            if (c == null) { Console.WriteLine("Not found."); break; }
                            Console.Write("New Full Name: ");
                            string n = Console.ReadLine();
                            Console.Write("New Date of Birth (yyyy-MM-dd): ");
                            DateTime d;
                            if (!DateTime.TryParse(Console.ReadLine(), out d)) { Console.WriteLine("Invalid date."); break; }
                            c.updateCustomerDetails(n, d);
                            Console.WriteLine("Updated.");
                            break;
                        }
                    case "3":
                        {
                            Console.Write("Customer ID: ");
                            int id;
                            if (!int.TryParse(Console.ReadLine(), out id)) { Console.WriteLine("Invalid ID."); break; }
                            bool ok = bank.RemoveCustomer(id);
                            Console.WriteLine(ok ? "Customer removed." : "Cannot remove (check balances) or not found.");
                            break;
                        }
                    case "4":
                        {
                            Console.Write("Search text (name or national ID): ");
                            string q = Console.ReadLine();
                            var results = bank.SearchCustomers(q);
                            if (results.Count == 0) { Console.WriteLine("No results."); break; }
                            foreach (var c in results)
                            {
                                Console.WriteLine("ID: " + c.Id + " | Name: " + c.FullName + " | NationalID: " + c.NationalID);
                            }
                            break;
                        }
                    case "5":
                        {
                            Console.Write("Customer ID: ");
                            int id;
                            if (!int.TryParse(Console.ReadLine(), out id)) { Console.WriteLine("Invalid ID."); break; }
                            Console.Write("Account Name: ");
                            string an = Console.ReadLine();
                            Console.Write("Type (Savings/Current): ");
                            string t = Console.ReadLine();

                            Console.Write("Opening Balance: ");
                            double ob;
                            if (!double.TryParse(Console.ReadLine(), out ob)) { Console.WriteLine("Invalid amount."); break; }

                            double rate = 0;
                            double od = 0;

                            if (t.ToLower() == "savings")
                            {
                                Console.Write("Interest Rate % (annual): ");
                                double.TryParse(Console.ReadLine(), out rate);
                            }
                            else if (t.ToLower() == "current")
                            {
                                Console.Write("Overdraft Limit: ");
                                double.TryParse(Console.ReadLine(), out od);
                            }

                            var acc = bank.AddAccountToCustomer(id, an, t, ob, rate, od);
                            if (acc == null) Console.WriteLine("Customer not found.");
                            else Console.WriteLine("Account created. Number = " + acc.AccountNumber);
                            break;
                        }
                    case "6":
                        {
                            Console.Write("Account Number: ");
                            int n;
                            if (!int.TryParse(Console.ReadLine(), out n)) { Console.WriteLine("Invalid."); break; }
                            Console.Write("Amount: ");
                            double amt;
                            if (!double.TryParse(Console.ReadLine(), out amt)) { Console.WriteLine("Invalid."); break; }
                            Console.WriteLine(bank.Deposit(n, amt) ? "Deposited." : "Failed.");
                            break;
                        }
                    case "7":
                        {
                            Console.Write("Account Number: ");
                            int n;
                            if (!int.TryParse(Console.ReadLine(), out n)) { Console.WriteLine("Invalid."); break; }
                            Console.Write("Amount: ");
                            double amt;
                            if (!double.TryParse(Console.ReadLine(), out amt)) { Console.WriteLine("Invalid."); break; }
                            Console.WriteLine(bank.Withdraw(n, amt) ? "Withdrawn." : "Failed (check balance/overdraft).");
                            break;
                        }
                    case "8":
                        {
                            Console.Write("From Account #: ");
                            int f;
                            if (!int.TryParse(Console.ReadLine(), out f)) { Console.WriteLine("Invalid."); break; }
                            Console.Write("To Account #: ");
                            int t;
                            if (!int.TryParse(Console.ReadLine(), out t)) { Console.WriteLine("Invalid."); break; }
                            Console.Write("Amount: ");
                            double amt;
                            if (!double.TryParse(Console.ReadLine(), out amt)) { Console.WriteLine("Invalid."); break; }
                            Console.WriteLine(bank.Transfer(f, t, amt) ? "Transferred." : "Failed (check accounts/balance).");
                            break;
                        }
                    case "9":
                        {
                            Console.Write("Customer ID: ");
                            int id;
                            if (!int.TryParse(Console.ReadLine(), out id)) { Console.WriteLine("Invalid."); break; }
                            Console.WriteLine("Total Balance = " + bank.GetCustomerTotalBalance(id).ToString("0.00"));
                            break;
                        }
                    case "10":
                        {
                            Console.Write("Account Number (Savings): ");
                            int n;
                            if (!int.TryParse(Console.ReadLine(), out n)) { Console.WriteLine("Invalid."); break; }
                            var acc = bank.FindAccountByNumber(n);
                            if (acc == null) { Console.WriteLine("Not found."); break; }
                            double interest = acc.CalculateMonthlyInterest();
                            Console.WriteLine("Monthly Interest = " + interest.ToString("0.00"));
                            break;
                        }
                    case "11":
                        {
                            bank.PrintBankReport();
                            break;
                        }
                    case "12":
                        {
                            Console.Write("Account Number: ");
                            int n;
                            if (!int.TryParse(Console.ReadLine(), out n)) { Console.WriteLine("Invalid."); break; }
                            bank.PrintAccountTransactions(n);
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

        }
    
    }
}
