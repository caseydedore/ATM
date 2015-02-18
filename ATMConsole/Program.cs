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

                //login 
                    
                    //user account operations loop
                
                //end login
            
            //end program



            //example operations and test

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
        }

        static void TEST_CurrentUserDetails()
        {
            List<int> userAccountsNums = atm.GetCurrentUserAccountNumbers();
            List<string> userAccountTypes = atm.GetCurrentUserAccountTypes();

            Console.WriteLine("----------------------------------------------------------------------");
            for (int i = 0; i < userAccountsNums.Count; i++)
            {
                Console.Write(userAccountsNums[i].ToString() + "    " + userAccountTypes[i] + "         ");
                Console.WriteLine(atm.GetCurrentUserBalance(userAccountsNums[i]));
            }
            Console.WriteLine("----------------------------------------------------------------------");
        }
    }
}
