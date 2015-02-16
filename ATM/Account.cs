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

        public Account() { }

        public Account(int acctNum)
        {
            accountNum = acctNum;
        }

        public decimal Balance { get { return balance; } set { balance = value; } }

        public int AccountNum { get { return accountNum; } set { accountNum = value; } }

        public bool Withdraw(decimal withdrawal)
        {
            balance -= withdrawal;
            return true;
        }

        public bool Deposit(decimal deposit)
        {
            balance += deposit;
            return true;
        }

        public decimal CheckBalance()
        {
            return balance;
        }
    }


}
