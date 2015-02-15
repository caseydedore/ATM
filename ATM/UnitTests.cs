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
        public int testAcctNum01,
                   testAcctNum02,
                   testAcctNum03;
        User user;


        [SetUp]
        public void Setup()
        {
            user = new User();

            //setup accounts for the test user
            testAcctNum01 = 10101010;
            testAcctNum02 = 11111111;
            testAcctNum03 = 11000001;
            user.CreateAccount(testAcctNum01);
            user.CreateAccount(testAcctNum02);
            user.CreateAccount(testAcctNum03);
        }

        [Test]
        public void TestUserAccountCreation()
        {
            //insert the number of CreateAccount statements called during Setup to see if they all have been added
            Assert.AreEqual(3, user.Accnts.Count);
        }

        [Test]
        public void TestUserAccountBalances()
        {
            //is the correct balance coming back?

            //alter the balance of the second user account
            Account accnt = user.Accnts[1];
            if (accnt != null)
            {
                accnt.Balance = 102.12m;
                Assert.AreEqual(102.12m, user.CheckBalance(user.Accnts[1].AccountNum));
            }
            else Assert.IsTrue(false);

            //does the user method for checking balance match the balance property within the account itself?
            foreach (Account act in user.Accnts)
            {
                Assert.AreEqual(act.Balance, user.CheckBalance(act.AccountNum));
            }
        }

        [Test]
        public void TestWithdraw()
        {
            //test to make sure the correct amount is being withdrawn from the correct account

            //test for a successful transaction that leaves no money left
            user.Accnts[0].Balance = 100m;
            user.Withdraw(user.Accnts[0].AccountNum, 100m);
            Assert.AreEqual(0, user.Accnts[0].Balance);

            //test for an unsuccessful transaction where there is insufficient funds in the account
            user.Accnts[0].Balance = 0m;
            Assert.False(user.Withdraw(user.Accnts[0].AccountNum, 123m));

            //test to make sure logic blocks the ability to withdraw 0 funds
            user.Accnts[0].Balance = 10m;
            Assert.False(user.Withdraw(user.Accnts[0].AccountNum, 0m));

            //test to make sure no negative withdrawls may occur
            user.Accnts[0].Balance = 350m;
            Assert.False(user.Withdraw(user.Accnts[0].AccountNum, -10m));
            Assert.AreEqual(350m, user.Accnts[0].Balance);
        }

        [Test]
        public void TestDeposit()
        {
            //make sure correct amount is being deposited into the correct account

            user.Accnts[1].Balance = 0m;
            user.Deposit(user.Accnts[1].AccountNum, 100m);
            Assert.AreEqual(100m, user.Accnts[1].Balance);

            //do a second test depositing money to acct where money exists already
            user.Accnts[1].Balance = 900m;
            user.Deposit(user.Accnts[1].AccountNum, 100m);
            Assert.AreEqual(1000m, user.Accnts[1].Balance);

            //make sure that no negative values may cause success
            user.Accnts[1].Balance = 900m;
            Assert.False(user.Deposit(user.Accnts[1].AccountNum, -91m));
            Assert.AreEqual(900m, user.Accnts[1].Balance);
        }

        [Test]
        public void TestTransfer()
        {
            //test to make sure the amount specified is removed from the sender acct and added to the receiver acct
        }  
    }
}
