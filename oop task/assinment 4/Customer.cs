using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practical
{
    internal class Customer
    {
        public int Id { get; }
        public string FullName { get; private set; }
        public string NationalID { get; }
        public DateTime DateOfBirth { get; private set; }
        public List<Account> Accounts { get; } = new List<Account>();

        public Customer(int id, string fullName, string nationalID, DateTime dateOfBirth)
        {
            Id = id;
            FullName = fullName;
            NationalID = nationalID;
            DateOfBirth = dateOfBirth;
        }

        public void updateCustomerDetails(string fullname, DateTime bod)
        {
            FullName = fullname;
            DateOfBirth = bod;
        }

        public bool canBeRemove()
        {
            foreach (Account ac in Accounts)
            {
                if (ac.Balance != 0)
                    return false;
            }
            return true;
        }
    }
}
