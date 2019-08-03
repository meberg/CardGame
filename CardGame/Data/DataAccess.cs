using CardGame.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Data
{
    public class DataAccess
    {
        GameContext context = new GameContext();

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        internal List<string> GetAllUserNames()
        {
            List<string> userNames = new List<string>();
            foreach (var user in context.Users.ToList())
            {
                userNames.Add(user.Username.ToLower());
            }
            return userNames;
        }

        internal bool DoesUserExist(string userName)
        {
            List<string> userNames = GetAllUserNames();
            if (userNames.Contains(userName.Trim().ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal User CreateUser(string userName, string passWord)
        {
            User user = new User();
            user.Username = userName;
            user.Password = passWord;
            user.AccountCreationDate = DateTime.Now;
            context.Users.Add(user);
            context.SaveChanges();

            User returnedUser = GetUserByUsername(userName);
            return returnedUser;
        }

        private User GetUserByUsername(string userName)
        {
            return context.Users.SingleOrDefault(a => a.Username == userName);
        }
    }
}
