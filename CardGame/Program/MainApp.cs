using CardGame.ClassLibrary;
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
        ConsoleHelper ch = new ConsoleHelper();
        public static string user = "Unknown";
        static string pass = "";

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


                Console.WriteLine("f) Play a game");

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
                        DisplayUsernameAndPassword();
                        break;
                    case ConsoleKey.D:
                        break;
                    case ConsoleKey.E:
                        break;
                    case ConsoleKey.F:
                        PlayGameMenu();
                        break;
                    case ConsoleKey.X:
                        Console.WriteLine("Are you sure you want to quit? [Y/N]");

                        ConsoleKey key1 = Console.ReadKey(true).Key;

                        if (key1 == ConsoleKey.Y)
                        {
                            Console.WriteLine("Quitting program...");
                            Thread.Sleep(1500);
                            Environment.Exit(0);
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
            

        }

        private void DisplayUsernameAndPassword()
        {
            Console.Clear();
            Console.WriteLine("Username: " + user);
            Console.WriteLine("Password: " + pass);
            ch.PressKeyToContinue();
        }

        private void LoginScreen()
        {
            string userName;
            string passWord = "";

            Console.Clear();
            Console.Write("Username: ");
            userName = Console.ReadLine();

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

            bool done = false;

            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine("Save username and password? [Y/N]");

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.Y:
                        done = true;
                        user = userName;
                        pass = passWord;
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