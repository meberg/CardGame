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
                userNames.Add(user.Username.ToLower());
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
            user.Username = userName;
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

        internal int GetHighScore(User currentUser, int gameId)
        {
            UserScore userScore = context.UserScores.SingleOrDefault(a => a.GameId == gameId && a.UserId == currentUser.Id);

            if (userScore is null)
            {
                UserScore userScore1 = new UserScore();
                userScore1.GameId = gameId;
                userScore1.UserId = currentUser.Id;
                userScore1.Score = 0;
                context.UserScores.Add(userScore1);
                context.SaveChanges();
            }
            return context.UserScores.SingleOrDefault(a => a.GameId == gameId && a.UserId == currentUser.Id).Score;
        }

        internal void SetNewHighscore(int score, User currentUser, int gameId)
        {
            context.UserScores.SingleOrDefault(a => a.GameId == gameId && a.UserId == currentUser.Id).Score = score;
            context.SaveChanges();
        }

        internal List<UserScore> GetHighScores(int gameId)
        {
            List<UserScore> highScoreList = context.UserScores.Where(a => a.GameId == gameId).Include(a => a.User).ToList();
            return highScoreList.OrderByDescending(a => a.Score).ToList();
        }
    }
}
