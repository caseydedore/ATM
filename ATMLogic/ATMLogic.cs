using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLogic
{
    public class ATM
    {
        //this is the user that is currently being used (or null)
        private User currentUser { get; set; }
        //this is the standin for a database of users
        private UserData userDataLayer = new UserData();


        public bool Login(int pin)
        {
            currentUser = userDataLayer.GetUser(pin);
            return currentUser != null;
        }

        public bool Logout()
        {
            currentUser = null;
            return true;
        }

        public bool Withdraw(int accntNum, decimal amnt)
        {
            if(currentUser == null) return false;

            return currentUser.Withdraw(accntNum, amnt);
        }

        public bool Deposit(int accntNum, decimal amnt)
        {
            if (currentUser == null) return false;

            return currentUser.Deposit(accntNum, amnt);
        }

        public bool Transfer(int senderAccntNum, int receiverAccntNum, decimal amnt)
        {
            if (currentUser == null) return false;

            return currentUser.Transfer(senderAccntNum, receiverAccntNum, amnt);
        }

        public decimal GetCurrentUserBalance(int acctNum)
        {
            if (currentUser == null) return 0;
            return currentUser.CheckBalance(acctNum);
        }

        public List<int> GetCurrentUserAccountNumbers()
        {
            if (currentUser == null) return null;
            return currentUser.GetAccountNumbers();
        }

        public List<string> GetCurrentUserAccountTypes()
        {
            if (currentUser == null) return null;
            return currentUser.GetAccountTypes();
        }
    }
}
