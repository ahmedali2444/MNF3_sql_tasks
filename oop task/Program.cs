using System;

public class BankAccount
{
    public const string BankCode = "mycode";

    public readonly DateTime CreatedDate;

    private int account_number;
    private string full_name;
    private string nat_id;
    private string phone_number;
    private string address;
    private decimal balance;

    private static int nextAccountNumber = 1000;


    public string FullName
    {
        get
        {
            return full_name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
            {
                Console.WriteLine("invalid input");
            }
            else
                full_name = value;
        }
    }

    public string NationalID
    {
        get
        {
            return nat_id;
        }
        set
        {
            if (value.Length != 14 || !ulong.TryParse(value, out _))
                Console.WriteLine("National ID must be exactly 14 digits.");
            else
                nat_id = value;
        }
    }

    public string PhoneNumber
    {
        get
        {
            return phone_number;
        }
        set
        {
            if (value.Length != 11 || !value.StartsWith("01") || !ulong.TryParse(value, out _))
                Console.WriteLine("Phone number must start with '01' and be 11 digits.");
            else
                phone_number = value;
        }
    }

    public decimal Balance
    {
        get
        {
            return balance;
        }
        set
        {
            if (value < 0)
                Console.WriteLine("Balance cannot be negative.");
            else
                balance = value;
        }
    }

    public string Address
    {
        get
        {
            return address;
        }
        set { address = value; }
    }


    public BankAccount()
    {
        account_number = nextAccountNumber++;
        FullName = "Unknown";
        NationalID = "00000000000000";
        PhoneNumber = "01000000000";
        Address = "N/A";
        Balance = 0;
        CreatedDate = DateTime.Now;
    }

    public BankAccount(string fullName, string nationalID, string phoneNumber, string address, decimal balance)
    {
        account_number = nextAccountNumber++;
        FullName = fullName;
        NationalID = nationalID;
        PhoneNumber = phoneNumber;
        Address = address;
        Balance = balance;
        CreatedDate = DateTime.Now;
    }

    public BankAccount(string fullName, string nationalID, string phoneNumber, string address)
    {
        FullName = fullName;
        NationalID = nationalID;
        PhoneNumber = phoneNumber;
        Address = address;
        Balance = 0;
    }


    public void ShowAccountDetails()
    {
        Console.WriteLine($"Account Number: {account_number}");
        Console.WriteLine($"Bank Code: {BankCode}");
        Console.WriteLine($"Full Name: {full_name}");
        Console.WriteLine($"National ID: {nat_id}");
        Console.WriteLine($"Phone Number: {phone_number}");
        Console.WriteLine($"Address: {address}");
        Console.WriteLine($"Balance: {balance:C}");
        Console.WriteLine($"Created On: {CreatedDate}\n------------\n");
    }

    public bool IsValidNationalID()
    {
        return nat_id.Length == 14 && ulong.TryParse(nat_id, out _);
    }

    public bool IsValidPhoneNumber()
    {
        return phone_number.Length == 11 && phone_number.StartsWith("01") && ulong.TryParse(phone_number, out _);
    }
}

class Program
{
    static void Main()
    {
        try
        {
            BankAccount acc1 = new BankAccount(
                "Ahmed Ali",
                "12345678901234",
                "01012345678",
                "Cairo, Egypt",
                1500.75m
            );

            BankAccount acc2 = new BankAccount(
                "Sara Mahmoud",
                "98765432109876",
                "01123456789",
                "Alexandria, Egypt"
            );

            acc1.ShowAccountDetails();
            acc2.ShowAccountDetails();

            Console.WriteLine($"Ahmed's National ID Valid? {acc1.IsValidNationalID()}");
            Console.WriteLine($"Sara's Phone Number Valid? {acc2.IsValidPhoneNumber()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

