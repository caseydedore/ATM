using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class User
    {
        private int pinNumber;
        private List<Account> accounts;

        public User() { }

        public bool Login(int inputPin)
        {
            return inputPin == pinNumber;
        }

        //public Account Accnt { get { return account; } set { account = value; } }

        public List<Account> Accnts
        {
            get
            { return accounts; }
            set
            { accounts = value; }
        }

        public void CreateAccount()
        {
            //accounts.Add(account);
        }

        public int PinNumber
        {
            get { return pinNumber; }
            set { pinNumber = value; }
        }

        public bool Transfer(Account n1, Account n2, decimal amnt) 
        {
            bool success = n1.Withdraw(amnt);
            if(success) success = n2.Deposit(amnt);
            return success;
        }
    }
}
