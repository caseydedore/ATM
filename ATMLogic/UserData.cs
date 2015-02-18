using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLogic
{
    internal class UserData
    {
        private List<User> users = new List<User>();

        /// <summary>
        /// This ctor is only here to populate the user list so there is something
        /// for users to test with.
        /// </summary>
        public UserData()
        {
            User tmp = new User{PinNumber = 1313};
            tmp.Accounts.Add(new Account(){AccountNum = 10928463, Type = "Savings", Balance = 1910.23m});
            tmp.Accounts.Add(new Account(){AccountNum = 10505014, Type = "Checking", Balance = 902.11m});
            users.Add(tmp);

            tmp = new User{PinNumber = 1653};
            tmp.Accounts.Add(new Account(){AccountNum = 15864876, Type = "Savings", Balance = 14646506.90m});
            tmp.Accounts.Add(new Account(){AccountNum = 12301230, Type = "Savings", Balance = 403209.01m});
            tmp.Accounts.Add(new Account(){AccountNum = 14996999, Type = "Checking", Balance = 1884.30m});
            users.Add(tmp);

            tmp = new User{PinNumber = 2198};
            tmp.Accounts.Add(new Account(){AccountNum = 19878654, Type = "Savings", Balance = 984.15m});
            users.Add(tmp);
        }

        public User GetUser(int pin)
        {
            foreach (User u in users)
            {
                if (u.PinNumber == pin) return u;
            }

            return null;
        }

        /// <summary>
        /// Returns all user pins and it only exists because we do not care about security.
        /// It is useful for displaying pins so that a user may test system functionality
        /// without memorizing user pins.
        /// </summary>
        /// <returns></returns>
        public List<int> GetUserPins()
        {
            List<int> pins = new List<int>();

            foreach(User u in users)
            {
                pins.Add(u.PinNumber);
            }
            return pins;
        }
    }
}
