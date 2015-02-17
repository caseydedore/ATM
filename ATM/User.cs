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
        private List<Account> accounts = new List<Account>();

        public User() { }        

        public List<Account> Accounts
        {
            get
            { return accounts; }
            set
            { accounts = value; }
        }

        public int PinNumber
        {
            get { return pinNumber; }
            set { pinNumber = value; }
        }

        public bool Login(int inputPin)
        {
            return inputPin == pinNumber;
        }

        public void CreateAccount(int acctNum)
        {
            //will the acctNum be auto-generated externally? 
            /*Don't think it matters really, because at a real ATM you wouldn't create an account.
              We just need to in this instance to generate test data. JW*/

            Account acct = new Account { AccountNum = acctNum };
            accounts.Add(acct);
        }        

        public bool Transfer(int acctNumSend, int acctNumReceive, decimal amnt) 
        {
            //find the accts with the matching nums. Withdraw from one and deposit to another the specified amount

            /* This is how it might look (n1 and n2 are the sender and receiver accounts)
             * 
            bool success = n1.Withdraw(amnt);
            if(success) success = n2.Deposit(amnt);
            return success;
            */
            //This looks nice, I like it, not quite sure how to implement yet though. -JW

            if ((acctNumSend != acctNumReceive) && Withdraw(acctNumSend, amnt))
            {
                if (Deposit(acctNumReceive, amnt))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Withdraw(int acctNum, decimal amnt)
        {
            //find the account matching the param number, withdraw

            decimal startBal;
            bool success = false;

            foreach(Account a in accounts)
            {
                if (a.AccountNum == acctNum && amnt > 0)
                {
                    startBal = a.Balance;
                     success = a.Withdraw(amnt);
                    //if (a.Balance == (startBal - amnt))
                    //{
                    //    success = true;
                    //}
                    break;
                }                
            }
           return success;
        }

        public bool Deposit(int acctNum, decimal amnt)
        {
            //find the account matching the param number, deposit specified amnt

            decimal startBal;
            bool success = false;

            foreach (Account a in accounts)
            {
                if (a.AccountNum == acctNum && amnt > 0)
                {
                    startBal = a.Balance;
                    a.Deposit(amnt);
                    if (a.Balance == startBal + amnt)
                    {
                        success = true;   
                    }
                    break;
                }
            }

            return success;
        }

        public decimal CheckBalance(int acctNum)
        {
            //this will find the account matching the acct number and ask it to return its balance. That balance will be returned from here
            decimal balance = 0;
            foreach (Account a in accounts)
            {
                if (a.AccountNum == acctNum)
                {
                    balance = a.CheckBalance();
                    break;
                }
            }
            return balance;
        }
    }
}
