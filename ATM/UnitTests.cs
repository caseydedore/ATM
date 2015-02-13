using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ATM
{
    [TestFixture]
    class UT_Account
    {
        [Test]
        public void TestCheckBalance()
        {
            //is the correct balance coming back?
            //Assert.AreEqual();
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

    [TestFixture]
    class UT_User
    {
        [Test]
        public void TestUserAccounts()
        {
           //create a few test users that do not have accts and others that do. Make sure that the users that have accts have accts 0.0
        }

        [Test]
        public void TestTransfer()
        {
            //does the correct amount of money get removed from the specified acct and added to the destination acct
            //does the operation fail if there is not enough money in the first acct to withdraw from
        }

        [Test]
        public void TestUserLogin
        {
            //create user with specified PIN. Test the login functionality to see if the specified PIN allows login
        }
    }
}
