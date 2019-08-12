using CardGame.ClassLibrary;
using CardGame.Data;
using CardGame.Games;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CardGame
{
    internal class MainApp
    {
        DataAccess dataAccess = new DataAccess();

        ConsoleHelper ch = new ConsoleHelper();
        public static User currentUser = new User() { Username = "No user is logged in", Password = "", AccountCreationDate = DateTime.Now };

        public static bool isLoggedIn = false;

        public MainApp()
        {
        }

        internal void Run()
        {
            ch.ResetColor();
            ch.Menu("Main menu");

            while (true)
            {
                Console.Clear();

                Console.WriteLine("What do you want to do?");

                Console.WriteLine("a) Log in");
                Console.WriteLine("b) Log out");
                Console.WriteLine("f) Play a game");
                Console.WriteLine("g) Show hiscores");
                Console.WriteLine("h) User management");

                Console.WriteLine("x) Quit");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        LoginScreen();
                        break;
                    case ConsoleKey.B:
                        Logout();
                        break;
                    case ConsoleKey.F:
                        PlayGameMenu();
                        break;
                    case ConsoleKey.G:
                        HiscoreMenu();
                        break;
                    case ConsoleKey.H:
                        UserManagement();
                        break;
                    case ConsoleKey.X:
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to quit? [Y/N]");

                        ConsoleKey key1 = Console.ReadKey(true).Key;

                        if (key1 == ConsoleKey.Y)
                        {
                            Console.WriteLine("Quitting program...");
                            Thread.Sleep(1500);
                            Console.WriteLine();
                            Environment.Exit(0);
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
            

        }

        private void UserManagement()
        {
            Console.Clear();
            bool done = false;

            while (!done)
            {
                Console.WriteLine("What do you want to do?");

                Console.WriteLine();
                Console.WriteLine("1) Display username and password");
                Console.WriteLine("2) Create new account");
                Console.WriteLine("3) Delete account");
                Console.WriteLine("4) Display all accounts");


                Console.WriteLine("x) Exit menu");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        DisplayAccountInfo();
                        Console.Clear();
                        break;
                    case ConsoleKey.D2:
                        CreateNewAccount();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                        DeleteAccount();
                        Console.Clear();
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        DisplayAllAccounts();
                        Console.WriteLine();
                        ch.PressKeyToContinue();
                        Console.Clear();
                        break;
                    case ConsoleKey.X:
                        done = true;
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        private void DeleteAccount()
        {
            Console.Clear();

            while (true)
            {
                DisplayAllAccounts();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Which account do you want to delete?");
                Console.Write("Enter username (or leave blank to exit): ");

                string userName = Console.ReadLine();

                if (userName == "")
                {
                    break;
                }
                else if (dataAccess.DoesUserExist(userName))
                {
                    dataAccess.DeleteUser(userName);
                    Console.Clear();
                    Console.WriteLine("User deleted");
                }
                else
                {
                    Console.WriteLine("No user with that name exists");
                }
                ch.PressKeyToContinue();
                Console.Clear();
            }
        }

        private void DisplayAllAccounts()
        {
            List<User> users = dataAccess.GetAllUsers();

            Console.WriteLine($"{"Id".PadRight(5)} {"Username".PadRight(20)}");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id.ToString().PadRight(5)} {user.Username.PadRight(20)}");
            }
        }

        private void LoginScreen()
        {
            string userName;
            string passWord = "";

            Console.Clear();
            Console.Write("Username: ");
            userName = Console.ReadLine();
            if (userName == "")
            {
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.Write("Username: ");
                Console.WriteLine(userName);
                Console.Write("Password: ");
                foreach (var letter in passWord)
                {
                    Console.Write("*");
                }

                char key = Console.ReadKey(true).KeyChar;

                if (key == 13)
                {
                    break;
                }
                if (key == 8)
                {
                    passWord = passWord.Substring(0, passWord.Length - 1);
                }
                else
                {
                    passWord = passWord + key.ToString();
                }
            }
            Console.WriteLine();
            if (dataAccess.IsCredentialsValid(userName, passWord))
            {
                currentUser = dataAccess.GetUserByUsername(userName);
                isLoggedIn = true;
                ch.Green("Login successful");
                Console.WriteLine();
                ch.PressKeyToContinue();
            }
            else
            {
                if (dataAccess.DoesUserExist(userName))
                {
                    ch.Red("Invalid password");
                    Console.WriteLine();
                    ch.PressKeyToContinue();
                }
                else
                {
                    ch.Red($"Username \"{userName}\" does not exist");
                    Console.WriteLine();
                    ch.PressKeyToContinue();
                }
                LoginScreen();
            }
        }

        private void Logout()
        {
            Console.Clear();
            Console.WriteLine($"{currentUser.Username} has been logged out");
            isLoggedIn = false;
            currentUser = new User() { Username = "No user is logged in", Password = "", AccountCreationDate = DateTime.Now };
            ch.PressKeyToContinue();
        }

        private void DisplayAccountInfo()
        {
            Console.Clear();
            Console.WriteLine("Username: " + currentUser?.Username);
            Console.WriteLine("Password: " + currentUser?.Password);
            Console.WriteLine($"Account created: {currentUser?.AccountCreationDate:F}");
            ch.PressKeyToContinue();
        }

        private void CreateNewAccount()
        {
            string userName = CreateUsername();
            if (userName == null)
            {
                return;
            }
            string passWord = CreatePassword(userName);

            bool done = false;

            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine($"Save username \"{userName}\" and password? [Y/N]");

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Y:
                        done = true;
                        User user = dataAccess.CreateUser(userName, passWord);
                        currentUser = user;
                        Console.WriteLine($"User \"{userName}\" created.");
                        isLoggedIn = true;
                        ch.PressKeyToContinue();
                        break;
                    case ConsoleKey.N:
                        done = true;
                        break;
                    default:
                        ch.Red("Press \"y\" or \"n\".");
                        ch.BlankLine();
                        break;
                }
            }
        }

        private string CreateUsername()
        {
            bool invalidUsername = true;
            string userName;

            do
            {
                Console.Clear();
                Console.Write("Username: ");
                userName = Console.ReadLine();
                if (userName == "")
                {
                    return null;
                }
                if (dataAccess.DoesUserExist(userName))
                {
                    Console.WriteLine("Username already exists, try a new one");
                    ch.PressKeyToContinue();
                }
                else
                {
                    invalidUsername = false;
                }

            } while (invalidUsername);

            return userName;
        }

        private string CreatePassword(string userName)
        {
            string passWord = "";

            do
            {
                Console.Clear();
                Console.Write("Username: ");
                Console.WriteLine(userName);
                Console.Write("Password: ");
                foreach (var letter in passWord)
                {
                    Console.Write("*");
                }

                char key = Console.ReadKey(true).KeyChar;

                if (key == 13)
                {
                    break;
                }
                if (key == 8)
                {
                    passWord = passWord.Substring(0, passWord.Length - 1);
                }
                else
                {
                    passWord = passWord + key.ToString();
                }
            } while (true);

            return passWord;
        }

        private void PlayGameMenu()
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                Console.Clear();

                Console.WriteLine("Which game do you want to play?");

                Console.WriteLine("a) Higher or Lower");
                Console.WriteLine("b) Turning ten (Not implemented)");
                Console.WriteLine("c) Snake");


                Console.WriteLine("x) Back to main menu");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        HigherOrLower higherOrLower = new HigherOrLower();
                        higherOrLower.Run();
                        break;
                    case ConsoleKey.B:
                        TurningTen turningTen = new TurningTen();
                        turningTen.Run();
                        break;
                    case ConsoleKey.C:
                        Snake.Run();
                        break;
                    case ConsoleKey.D:
                        break;
                    case ConsoleKey.E:
                        break;
                    case ConsoleKey.F:
                        break;
                    case ConsoleKey.X:
                        keepGoing = false;
                        Run();
                        break;
                    default:
                        break;
                }
            }
        }

        private void HiscoreMenu()
        {
            bool done = false;

            while (!done)
            {
                Console.Clear();

                Console.WriteLine("For which game do you want to show the hiscore?");

                Console.WriteLine();
                Console.WriteLine("1) HigherOrLower");
                Console.WriteLine("2) Snake 2");
                Console.WriteLine("3) Psychadelic snake");
                Console.WriteLine("x) Exit menu");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        ShowHiscore(HigherOrLower.GameId);
                        Console.WriteLine();
                        ch.PressKeyToContinue();
                        Console.Clear();
                        break;
                    case ConsoleKey.D2:
                        ShowHiscore(Snake.GameId);
                        Console.WriteLine();
                        ch.PressKeyToContinue();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                        ShowHiscore(Snake.GameIdPsych);
                        Console.WriteLine();
                        ch.PressKeyToContinue();
                        Console.Clear();
                        break;
                    case ConsoleKey.X:
                        done = true;
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowHiscore(int gameId)
        {
            string[] gameNames = { "none", "HigherOrLower", "Snake 2", "Psychadelic snake" };

            Console.Clear();
            ch.Menu(gameNames[gameId]);

            List<UserScore> hiscore = dataAccess.GetUserScores(gameId);
            Console.WriteLine("Rank".PadRight(5) + "Username".PadRight(20) + "Score".PadRight(10) + "Accuracy");
            int counter = 1;
            foreach (var score in hiscore)
            {
                Console.WriteLine(counter.ToString().PadRight(5) + score.User.Username.PadRight(20) + score.Score.ToString().PadRight(10) + score.TimePerFood.ToString());
                counter++;
            }
        }
    }
}