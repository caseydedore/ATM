using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Account
    {
        private decimal balance;
        private int accountNum;
        private string type = "Savings";

        public Account() { }

        public Account(int acctNum)
        {
            accountNum = acctNum;
        }

        public decimal Balance { get { return balance; } set { balance = value; } }

        public int AccountNum { get { return accountNum; } set { accountNum = value; } }

        public string Type { get { return type; } set { type = value; } }

        public bool Withdraw(decimal withdrawal)
        {
            if (balance >= withdrawal)
            {
                balance -= withdrawal;
                return true;
            }
            return false;
        }

        public bool Deposit(decimal deposit)
        {
            if (deposit > 0)
            {
                balance += deposit;
                return true;
            }
            return false;
        }

        public decimal CheckBalance()
        {
            return balance;
        }
    }


}
