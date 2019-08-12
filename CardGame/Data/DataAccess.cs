using CardGame.ClassLibrary;
using Microsoft.EntityFrameworkCore;
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

        public List<string> GetAllUserNames()
        {
            List<string> userNames = new List<string>();
            foreach (var user in context.Users.ToList())
            {
                userNames.Add(user.Username.Trim().ToLower());
            }
            return userNames;
        }

        public bool DoesUserExist(string userName)
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

        public User CreateUser(string userName, string passWord)
        {
            User user = new User();
            user.Username = userName.Trim().ToLower();
            user.Password = passWord;
            user.AccountCreationDate = DateTime.Now;
            context.Users.Add(user);
            context.SaveChanges();

            User returnedUser = GetUserByUsername(userName);
            return returnedUser;
        }

        public User GetUserByUsername(string userName)
        {
            return context.Users.SingleOrDefault(a => a.Username == userName);
        }

        internal bool IsCredentialsValid(string userName, string passWord)
        {
            if (DoesUserExist(userName))
            {
                User user = GetUserByUsername(userName);
                if (user.Password == passWord)
                {
                    return true;
                }
            }
            return false;
        }

        internal List<UserScore> GetUserScores(int gameId)
        {
            List<UserScore> highScoreList = context.UserScores.Where(a => a.GameId == gameId).Include(a => a.User).ToList();
            return highScoreList.OrderByDescending(a => a.Score).ToList();
        }

        internal void DeleteUser(string userName)
        {
            User user = GetUserByUsername(userName.Trim().ToLower());
            context.Remove(user);
            context.SaveChanges();
        }

        internal UserScore GetUserScore(User currentUser, int gameId)
        {
            List<UserScore> userScores = GetUserScores(gameId);
            if (!userScores.Exists(a => a.UserId == currentUser.Id && a.GameId == gameId))
            {
                UserScore userScore = new UserScore();
                userScore.UserId = currentUser.Id;
                userScore.GameId = gameId;
                context.UserScores.Add(userScore);
                context.SaveChanges();
            }
            return context.UserScores.SingleOrDefault(a => a.User == currentUser && a.GameId == gameId);
        }

        internal void UpdateUserScore(UserScore userScore)
        {
            context.UserScores.Update(userScore);
            context.SaveChanges();
        }
    }
}
