using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMLogic;

namespace ATMConsole
{
    class Program
    {
        //this is the only reference needed to implement all ATM logic (if any extra ATMLogic functionality is added later it must be called through here)
        private static ATM atm = new ATM();

        static void Main(string[] args)
        {



            //program loop
            //Display login options
                //login choice 
                    //Display login success 
                    //Display user choices
                        //CHECK BALANCE
                            //
                        //WITHDRAW
                            //
                        //DEPOSIT
                            //
                        //TRANSFER
                            //
                        //LOGOUT
                            //
                //end login
            
            //end program

            bool mainLoop = true;

            do
            {
                StartMessage();
                //Login Loop
                bool loginLoop = false;
                bool loggedIn = false;
                do
                {
                    loginLoop = LogInSequence();
                    loggedIn = loginLoop;
                }
                while (loginLoop == false);

                Console.WriteLine("Logged in successfully.\n");
                Console.WriteLine("press any key to continue");
                Console.ReadKey();
                Console.Clear();

                do
                {
                    loggedIn = ChoiceSequence();
                }
                while (loggedIn == true);

                Console.WriteLine("Choice Sequence exit.");

                Console.ReadKey();
                Console.Clear();
            } 
            while (mainLoop == true);

            //example operations and test

            /*
            Console.WriteLine("Logging in with pin 1313");
            Console.WriteLine(atm.Login(1313).ToString());

            TEST_CurrentUserDetails();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            atm.Logout();
            
            Console.WriteLine("Logging in with pin 1653");
            Console.WriteLine(atm.Login(1653).ToString());

            TEST_CurrentUserDetails();

            atm.Logout();
            Console.ReadKey();
             * */
        }

        static void TEST_CurrentUserDetails()
        {
            List<int> userAccountsNums = atm.GetCurrentUserAccountNumbers();
            List<string> userAccountTypes = atm.GetCurrentUserAccountTypes();

            Console.WriteLine("----------------------------------------------------------------------");
            for (int i = 0; i < userAccountsNums.Count; i++)
            {
                Console.Write((i + 1) + ": " + userAccountsNums[i].ToString() + "    " + userAccountTypes[i] + "         ");
                Console.WriteLine(atm.GetCurrentUserBalance(userAccountsNums[i]));
            }
            Console.WriteLine("----------------------------------------------------------------------");
        }

        static bool ChoiceSequence() //TODO
        {
            Console.WriteLine("Please select an action by pressing a number.\n");
            Console.WriteLine("1. Check balance");
            Console.WriteLine("2. Withdraw Funds");
            Console.WriteLine("3. Deposit Funds");
            Console.WriteLine("4. Transfer Funds");
            Console.WriteLine("5. Log Out\n");

            int choiceInt = -1;
            string choiceInput = "";

            choiceInput = Console.ReadLine();

            try
            {
                choiceInt = Convert.ToInt32(choiceInput);
            }
            catch (FormatException e)
            {
                Console.WriteLine("The entered choice must be a single number.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The entered value is too large.");
            }

            Console.WriteLine();

            if (choiceInt == 1)
            { CheckBalanceSequence(); }
            else if (choiceInt == 2)
            { WithdrawalSequence(); }
            else if (choiceInt == 3)
            { DepositSequence(); }
            else if (choiceInt == 4)
            { TransferSequence();}
            else if (choiceInt == 5)
            { return !LogoutSequence(); }
            else
            {
                Console.WriteLine("Please enter a valid choice.");
            }
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
            Console.Clear();
            return true;
        }


        static bool LogInSequence() //Done
        {
            int pinInt= -1;
            bool validPin = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a PIN.");
                string pinInput = Console.ReadLine();
                try
                {
                    pinInt = Convert.ToInt32(pinInput);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("The entered PIN must be only numbers.");
                }
                catch (OverflowException e)
                {
                    Console.WriteLine("The entered value is too large.");
                }

                if (pinInt != -1) { validPin = true; }
                else { Console.WriteLine("Please enter a valid integer PIN"); }
            } while (validPin == false);

            if (atm.Login(pinInt) == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("LogIn failed, please try again.");
                return false;
            }
        }

        static void CheckBalanceSequence() //TODO
        {
            Console.WriteLine("*Check Balance Sequence*");
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
            Console.Clear();
            //Using caseys code from
            TEST_CurrentUserDetails();


        }

        static void WithdrawalSequence() //TODO
        {
            int selectedAccount = -1;
            string selectedAccountInput = "";

            decimal withdrawAmount = -1;
            string withDrawAmountInput = "";

            bool success = false;
            bool validAccount = true;
            bool validAmount = true;

            List<int> userAccountsNums = atm.GetCurrentUserAccountNumbers();
            List<string> userAccountTypes = atm.GetCurrentUserAccountTypes();

            do
            {
                do
                {
                    validAccount = true;

                    Console.Clear();
                    Console.WriteLine("*Withdrawal Sequence*");
                    Console.WriteLine("Please select an account from the numbered list below.");

                    for (int i = 0; i < userAccountsNums.Count; i++)
                    {
                        Console.Write((i + 1) + ": " + userAccountsNums[i].ToString() + "    " + userAccountTypes[i] + "         ");
                        Console.WriteLine(atm.GetCurrentUserBalance(userAccountsNums[i]));
                    }

                    selectedAccountInput = Console.ReadLine();

                    try
                    { selectedAccount = Convert.ToInt32(selectedAccountInput); }
                    catch (FormatException e)
                    { Console.WriteLine("The entered choice must be a number."); validAccount = false; }
                    catch (OverflowException e)
                    { Console.WriteLine("The entered value is too large."); validAccount = false; }

                    if (selectedAccount < 1 || selectedAccount > userAccountsNums.Count) { validAccount = false; }
                    if (validAccount == false) { Console.WriteLine("Please enter a valid selection."); Console.ReadKey(); }
                } while (validAccount == false);
                

                do
                {
                    validAmount = true;
                    Console.WriteLine("Please enter amount to withdraw.");
                    withDrawAmountInput = Console.ReadLine();

                    try
                    { withdrawAmount = Convert.ToDecimal(withDrawAmountInput); }
                    catch (FormatException e)
                    { Console.WriteLine("The entered choice must be a number."); validAmount = false; }
                    catch (OverflowException e)
                    { Console.WriteLine("The entered value is too large."); validAmount = false; }


                } while (validAmount == false);
                



                success = atm.Withdraw(userAccountsNums[selectedAccount - 1], withdrawAmount);

                if (success == false) { Console.WriteLine("Insufficient funds, please try again.\n"); }
            }
            while (success == false);

            

        }

        static void DepositSequence() //TODO
        {
            Console.WriteLine("*Deposit Sequence*");
        }
        static void TransferSequence() //TODO
        {
            Console.WriteLine("*Transfer Sequence*");
        }

        static bool LogoutSequence() //Done
        {
            Console.WriteLine("*Logout Sequence*");
            return atm.Logout();
        }

        static void StartMessage() //Done
        {
            Console.WriteLine("Welcome to our ATM project model ATM.");
            Console.WriteLine("To begin using this demo please log in");
            Console.WriteLine("using one of the prepared account pins,");
            Console.WriteLine("1313, 1653, or 2198.\n\n" );
        }

    }
}
