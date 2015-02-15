using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ATM
{
    [TestFixture]
    class UT_UserAccount
    {
        [Test]
        public void TestCheckUserAccountBalances()
        {
            //is the correct balance coming back?
            int tempNum = 10101010,
                tempNum02 = 11111111;
            User user = new User();
            user.CreateAccount(tempNum);
            user.CreateAccount(tempNum02);

            Account accnt = user.Accnts[1];
            if (accnt != null)
            {
                accnt.Balance = 102.12m;
            }
            else Assert.IsTrue(false);

            //does the user method for checking balance match the balance property in the account itself?
            foreach (Account act in user.Accnts)
            {
                Assert.AreEqual(act.Balance, user.CheckBalance(act.AccountNum));
            }
        }

        [Test]
        public void TestWithdraw()
        {
            //test to make sure the correct amount is being withdrawn from the correct account
        }

        [Test]
        public void TestDeposit()
        {
            //make sure correct amount is being deposited into the correct account
        }

        [Test]
        public void TestTransfer()
        {
            //test to make sure the amount specified is removed from the sender acct and added to the receiver acct
        }  
    }
}
