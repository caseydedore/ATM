﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ATMLogic
{
    [TestFixture]
    class UT_UserAccount
    {
        public int[] testAcctNums;
        User user;
        ATM testATM = new ATM();
        UserData uData = new UserData();


        [SetUp]
        public void Setup()
        {
            user = new User();

            //setup accounts for the test user
            testAcctNums = new int[]{ 1234, 1212, 1342, 1902 };
            //these accounts are created the hard way so that the tests are independent of the success of CreateUserAccount()
            //NOTE: do NOT initialize the last account for the acct number here. It will be done through a test
            for (int i = 0; i < testAcctNums.Count() - 1; i++)
            {
                user.Accounts.Add(new Account(testAcctNums[i]));
            }
        }

        [Test]
        public void TestUserAccountCreation()
        {
            //create an account using the account creation method in User. See if it was added
            user.CreateAccount(testAcctNums[testAcctNums.Count() - 1]);
            Assert.AreEqual(4, user.Accounts.Count);
        }

        [Test]
        public void TestUserAccountBalances()
        {
            //is the correct balance coming back? Do a single test first
            user.Accounts[0].Balance = 102.12m;
            Assert.AreEqual(102.12m, user.CheckBalance(user.Accounts[0].AccountNum));

            Random r = new Random();
            //check all accounts
            for (int i = 0; i < user.Accounts.Count(); i++)
            {
                user.Accounts[i].Balance = (decimal)r.Next(100000);
                Assert.AreEqual(user.Accounts[i].Balance, user.CheckBalance(user.Accounts[i].AccountNum));
            }
        }

        [Test]
        public void TestWithdraw()
        {
            //test to make sure the correct amount is being withdrawn from the correct account

            //test for a successful transaction that leaves no money left
            user.Accounts[0].Balance = 100m;
            user.Withdraw(user.Accounts[0].AccountNum, 100m);
            Assert.AreEqual(0, user.Accounts[0].Balance);

            //test for an unsuccessful transaction where there is insufficient funds in the account
            user.Accounts[0].Balance = 0m;
            Assert.False(user.Withdraw(user.Accounts[0].AccountNum, 123m));

            //test to make sure logic blocks the ability to withdraw 0 funds
            user.Accounts[0].Balance = 10m;
            Assert.False(user.Withdraw(user.Accounts[0].AccountNum, 0m));

            //test to make sure no negative withdrawls may occur
            user.Accounts[0].Balance = 350m;
            Assert.False(user.Withdraw(user.Accounts[0].AccountNum, -10m));
            Assert.AreEqual(350m, user.Accounts[0].Balance);
        }

        [Test]
        public void TestDeposit()
        {
            //make sure correct amount is being deposited into the correct account

            user.Accounts[1].Balance = 0m;
            user.Deposit(user.Accounts[1].AccountNum, 100m);
            Assert.AreEqual(100m, user.Accounts[1].Balance);

            //second test depositing money to acct where money exists already
            user.Accounts[1].Balance = 900m;
            user.Deposit(user.Accounts[1].AccountNum, 100m);
            Assert.AreEqual(1000m, user.Accounts[1].Balance);

            //make sure that no negative values may cause success
            user.Accounts[1].Balance = 900m;
            Assert.False(user.Deposit(user.Accounts[1].AccountNum, -91m));
            Assert.AreEqual(900m, user.Accounts[1].Balance);

            //depositing 0 - should not happen
            user.Accounts[1].Balance = 100m;
            Assert.False(user.Deposit(user.Accounts[1].AccountNum, 0));
            Assert.AreEqual(100m, user.Accounts[1].Balance);
        }

        /// <summary>
        /// This test and Transfer() itself relies on Deposit() and Withdraw()
        /// </summary>
        [Test]
        public void TestTransfer()
        {
            //test to make sure the amount specified is removed from the sender acct and added to the receiver acct

            //transfer funds from one account to another
            SetAllAccountBalances(ref user, 1000m);
            user.Transfer(user.Accounts[0].AccountNum, user.Accounts[1].AccountNum, 100m);
            Assert.AreEqual(900m, user.Accounts[0].Balance);
            Assert.AreEqual(1100m, user.Accounts[1].Balance);

            //transfer a small amount from one to another
            SetAllAccountBalances(ref user, 1m);
            user.Transfer(user.Accounts[0].AccountNum, user.Accounts[1].AccountNum, 0.03m);
            Assert.AreEqual(0.97m, user.Accounts[0].Balance);
            Assert.AreEqual(1.03m, user.Accounts[1].Balance);

            //transfer from where there are insufficient funds
            SetAllAccountBalances(ref user, 10m);
            Assert.False(user.Transfer(user.Accounts[0].AccountNum, user.Accounts[1].AccountNum, 100m));
            Assert.AreEqual(10m, user.Accounts[0].Balance);
            Assert.AreEqual(10m, user.Accounts[1].Balance);

            //transfer 0 - should NOT happen at all
            SetAllAccountBalances(ref user, 240m);
            Assert.False(user.Transfer(user.Accounts[0].AccountNum, user.Accounts[1].AccountNum, 0m));
            Assert.AreEqual(240m, user.Accounts[0].Balance);
            Assert.AreEqual(240m, user.Accounts[1].Balance);

            //transfer negative funds
            SetAllAccountBalances(ref user, 100m);
            Assert.False(user.Transfer(user.Accounts[0].AccountNum, user.Accounts[1].AccountNum, -20m));
            Assert.AreEqual(100m, user.Accounts[0].Balance);
            Assert.AreEqual(100m, user.Accounts[1].Balance);

            //transfer from and to the same account - should not happen
            SetAllAccountBalances(ref user, 100m);
            Assert.False(user.Transfer(user.Accounts[0].AccountNum, user.Accounts[0].AccountNum, 15.20m));
            Assert.AreEqual(100m, user.Accounts[0].Balance);

        }

        private void SetAllAccountBalances(ref User u, decimal d)
        {
            foreach (Account acct in u.Accounts)
            {
                acct.Balance = d;
            }
        }

        [Test]
        public void TestLoginLogout()
        {
            //This test will be used for the ATMLogic class. It contains the login method that will return a user

            Assert.False(testATM.Login(1312));
            //test log in with wrong pin

            Assert.True(testATM.Login(1313));
            //test log in with correct pin

            Assert.True(testATM.Logout());
            //test user log out
        }
               
        [Test]
        public void TestATMLogicWithoutCurrentUser()
        {
            // These will test that the functions of the ATM will not work while not logged in
            testATM.Logout();

            Assert.False(testATM.Withdraw(1313, 200));
            Assert.False(testATM.Deposit(1313, 200));
            Assert.False(testATM.Transfer(1313, 4321, 200));
            Assert.AreEqual(testATM.GetCurrentUserBalance(10505014), 0);
            Assert.IsNull(testATM.GetCurrentUserAccountNumbers());
            Assert.IsNull(testATM.GetCurrentUserAccountTypes());            
        }

         [Test]
        public void TestATMLogicWithCurrentUser()
        {
            testATM.Login(1313);  // Log in

            // These will test that the functions of the ATM WILL work while logged in

            Assert.True(testATM.Withdraw(10928463, 200));
            Assert.True(testATM.Deposit(10928463, 200));
            Assert.True(testATM.Transfer(10928463, 10505014, 200));
            Assert.Greater(testATM.GetCurrentUserBalance(10505014), 0);
            Assert.IsNotNull(testATM.GetCurrentUserAccountNumbers());
            Assert.IsNotNull(testATM.GetCurrentUserAccountTypes());
        }

        [Test]
        public void TestGetUser()
        {
            // The user's should match
            Assert.AreEqual(testATM.Login(1313), uData.GetUser(1313));

            // The user's shouldn't match 
            Assert.AreNotSame(testATM.Login(1653), uData.GetUser(1313));
        }
    }
}
