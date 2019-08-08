using CardGame.ClassLibrary;
using CardGame.Data;
using CardGame.Games;
using CardGame.TestClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace CardGame
{
    internal class MainApp
    {
        GameContext context = new GameContext();
        DataAccess dataAccess = new DataAccess();

        ConsoleHelper ch = new ConsoleHelper();
        public static User currentUser = new User() { Username = "Tester", Password = "UnKnown", AccountCreationDate = DateTime.Now };

        public static bool isLoggedIn = false;

        public MainApp()
        {
        }

        internal void Run()
        {
            ch.ResetColor();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("What do you want to do?");

                Console.WriteLine("a) Log in");
                Console.WriteLine("b) Log out (Not implemented)");
                Console.WriteLine("c) Display username and password");
                Console.WriteLine("d) Create new account");
                Console.WriteLine("f) Play a game");
                Console.WriteLine("g) Show hiscores");

                Console.WriteLine("x) Quit");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        LoginScreen();
                        break;
                    case ConsoleKey.B:
                        break;
                    case ConsoleKey.C:
                        DisplayAccountInfo();
                        break;
                    case ConsoleKey.D:
                        CreateNewAccount();
                        break;
                    case ConsoleKey.E:
                        break;
                    case ConsoleKey.F:
                        PlayGameMenu();
                        break;
                    case ConsoleKey.G:
                        HiscoreMenu();
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

        private void HiscoreMenu()
        {
            Console.Clear();
            bool done = false;

            while (!done)
            {
                Console.WriteLine("For which game do you want to show the hiscore?");

                Console.WriteLine();
                Console.WriteLine("1) HigherOrLower");
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
            Console.Clear();
            List<UserScore> hiscore = dataAccess.GetHighScores(gameId);
            Console.WriteLine("Rank".PadRight(5) + "Username".PadRight(20) + "Score");
            int counter = 1;
            foreach (var score in hiscore)
            {
                Console.WriteLine(counter.ToString().PadRight(5) + score.User.Username.PadRight(20) + score.Score);
                counter++;
            }
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

        // FUNCTIONAL Make CheckIfValidPassword(string password)-method (Perhaps also CheckIfValidUserName(string userName))
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

        // DONE
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

        // DONE
        private void DisplayAccountInfo()
        {
            Console.Clear();
            Console.WriteLine("Username: " + currentUser?.Username);
            Console.WriteLine("Password: " + currentUser?.Password);
            Console.WriteLine($"Account created: {currentUser?.AccountCreationDate:F}");
            ch.PressKeyToContinue();
        }

        // DONE
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

        // FUNCTIONAL
        private void PlayGameMenu()
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                Console.Clear();

                Console.WriteLine("Which game do you want to play?");

                Console.WriteLine("a) Higher or Lower");
                Console.WriteLine("b) Tian (Not implemented)");

                Console.WriteLine("x) Back to main menu");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        HigherOrLower higherOrLower = new HigherOrLower();
                        higherOrLower.Run();
                        break;
                    case ConsoleKey.B:
                        break;
                    case ConsoleKey.C:
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
    }
}