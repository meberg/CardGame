using CardGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame.Games
{
    public class Snake
    {
        static bool psychadelicGame = false;
        public static int GameId = 2;
        public static int GameIdPsych = 3;

        static bool gameOver = false;
        static int windowHeight = 20;
        static int windowWidth = 60;

        static int score = 0;

        static int currentXCoordinate = 8;
        static int currentXDirection = 0;
        static ConsoleKey currentDirection = ConsoleKey.B;
        static ConsoleKey oppositeDirection = ConsoleKey.A;

        static int currentYCoordinate = 5;
        static int currentYDirection = 0;

        // X, Y-coord
        static Tuple<int, int> foodLocation = new Tuple<int, int>(14, 13);
        static Tuple<int, int> currentLocation;

        static List<Tuple<int, int>> coordinateList = new List<Tuple<int, int>>();

        static ConsoleHelper ch = new ConsoleHelper();
        static DataAccess dataAccess = new DataAccess();

        public static void Run(bool psychadelic = false)
        {
            if (psychadelic)
            {
                psychadelicGame = true;
            }

            MainMenu();

        }

        private static void MainMenu()
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                Console.Clear();

                ch.Menu($"Welcome to Snake {MainApp.currentUser}!");
                ch.BlankLine(2);

                Console.WriteLine("What do you want to do?");

                Console.WriteLine("a) Start Snake 2");
                Console.WriteLine("b) Start Psychadelic snake");

                Console.WriteLine("x) Back to previous menu");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        psychadelicGame = false;
                        StartGame();
                        break;
                    case ConsoleKey.B:
                        psychadelicGame = true;
                        StartGame();
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
                        break;
                    default:
                        break;
                }
            }
        }

        private static void StartGame()
        {
            ResetValues();

            bool continuePlaying = true;

            while (continuePlaying)
            {
                RunGame();
                GameOver();
                Console.Clear();
                Console.WriteLine("Play again? [Y/N]");
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y)
                {
                    continuePlaying = true;
                }
                else if (key == ConsoleKey.N)
                {
                    continuePlaying = false;
                }
            }
        }

        private static void ResetValues()
        {
            gameOver = false;
            windowHeight = 20;
            windowWidth = 60;

            score = 0;

            currentXCoordinate = 8;
            currentXDirection = 0;
            currentDirection = ConsoleKey.B;
            oppositeDirection = ConsoleKey.A;

            currentYCoordinate = 5;
            currentYDirection = 0;

            // X, Y-coord
            foodLocation = new Tuple<int, int>(14, 13);

            coordinateList = new List<Tuple<int, int>>();
        }

        private static void RunGame()
        {
            Console.SetWindowSize(windowWidth + 1, windowHeight + 1);
            AddFoodLocation();

            while (!gameOver)
            {
                do
                {
                    Console.CursorVisible = false;
                    Console.Clear();

                    AddCurrentLocation();
                    if (psychadelicGame)
                    {
                        PrintGameAreaPsychadelic();
                    }
                    else
                    {
                        PrintGameArea();
                    }
                    ContinueInCurrentDirection();
                    Thread.Sleep(100 - score * 2);
                    if (gameOver)
                    {
                        break;
                    }
                } while (!Console.KeyAvailable);
                if (gameOver)
                {
                    break;
                }
                ChangeDirection();
            }
        }

        private static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over!");
            Console.WriteLine($"You got {score} points!");

            if (MainApp.isLoggedIn)
            {
                if (psychadelicGame)
                {
                    int highscore = dataAccess.GetHighScore(MainApp.currentUser, GameIdPsych);

                    if (score > highscore)
                    {
                        dataAccess.SetNewHighscore(score, MainApp.currentUser, GameIdPsych);
                        Console.WriteLine();
                        Console.WriteLine($"New highscore! Your previous highscore was {highscore} points.");
                    }
                    else
                    {
                        Console.WriteLine($"Too bad, you didn't beat your highscore. Your highscore is {highscore} points");
                    }
                }
                else
                {
                    int highscore = dataAccess.GetHighScore(MainApp.currentUser, GameId);

                    if (score > highscore)
                    {
                        dataAccess.SetNewHighscore(score, MainApp.currentUser, GameId);
                        Console.WriteLine();
                        Console.WriteLine($"New highscore! Your previous highscore was {highscore} points.");
                    }
                    else
                    {
                        Console.WriteLine($"Too bad, you didn't beat your highscore. Your highscore is {highscore} points");
                    }
                }
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        private static void AddFoodLocation()
        {
            coordinateList.Add(foodLocation);
        }

        private static void ContinueInCurrentDirection()
        {
            currentXCoordinate = currentXCoordinate + currentXDirection;
            currentYCoordinate = currentYCoordinate + currentYDirection;

            if (currentXCoordinate < 0)
            {
                currentXCoordinate = windowWidth - 2;
            }
            else if (currentXCoordinate > windowWidth - 1)
            {
                currentXCoordinate = 0;
            }
            else if (currentYCoordinate < 0)
            {
                currentYCoordinate = windowHeight - 1;
            }
            else if (currentYCoordinate > windowHeight - 1)
            {
                currentYCoordinate = 0;
            }
        }

        private static void AddCurrentLocation()
        {
            currentLocation = new Tuple<int, int>(currentXCoordinate, currentYCoordinate);
            if (coordinateList.Count > 2)
            {
                if (!coordinateList.GetRange(1, coordinateList.Count - 2).Contains(currentLocation))
                {
                    coordinateList.Add(currentLocation);
                    CheckIfSnakeGotFood(currentLocation);
                }
                else
                {
                    gameOver = true;
                }
            }
            else
            {
                coordinateList.Add(currentLocation);
                CheckIfSnakeGotFood(currentLocation);
            }
        }

        private static void CheckIfSnakeGotFood(Tuple<int, int> currentLocation)
        {
            if (currentLocation.Item1 == foodLocation.Item1 && currentLocation.Item2 == foodLocation.Item2)
            {
                NewFoodLocation();
                score++;
            }
            else
            {
                if (coordinateList.Count > 2)
                {
                    coordinateList.RemoveAt(1);
                }
            }
        }

        private static void NewFoodLocation()
        {
            Random random = new Random();

            int randomWidth = random.Next(windowWidth);
            if (randomWidth % 2 == 1)
            {
                randomWidth--;
            }

            int randomHeight = random.Next(windowHeight);

            Tuple<int, int> newFoodLocation = new Tuple<int, int>(randomWidth, randomHeight);
            if (newFoodLocation != foodLocation)
            {
                foodLocation = newFoodLocation;
                coordinateList.RemoveAt(0);
                coordinateList.Insert(0, newFoodLocation);
            }
            else
            {
                NewFoodLocation();
            }
        }

        private static void ChangeDirection()
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == currentDirection || key == oppositeDirection)
            {
                // Do nothing
            }
            else
            {
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        currentDirection = ConsoleKey.LeftArrow;
                        oppositeDirection = ConsoleKey.RightArrow;

                        currentXDirection = -2;
                        currentYDirection = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        currentDirection = ConsoleKey.RightArrow;
                        oppositeDirection = ConsoleKey.LeftArrow;

                        currentXDirection = 2;
                        currentYDirection = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        currentDirection = ConsoleKey.UpArrow;
                        oppositeDirection = ConsoleKey.DownArrow;

                        currentYDirection = -1;
                        currentXDirection = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        currentDirection = ConsoleKey.DownArrow;
                        oppositeDirection = ConsoleKey.UpArrow;

                        currentYDirection = 1;
                        currentXDirection = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void PrintGameArea()
        {
            List<Tuple<int, int>> tempList = coordinateList.OrderBy(a => a.Item2).ThenBy(b => b.Item1).ToList();
            int currentIndex = 0;
            int listCount = tempList.Count;

            for (int i = 0; i < windowHeight; i++)
            {
                if (listCount > currentIndex && tempList[currentIndex]?.Item2 == i)
                {
                    for (int j = 0; j < windowWidth + 1; j = j + 2)
                    {
                        if (coordinateList.Contains(new Tuple<int, int>(j, i)))
                        {
                            if (coordinateList.IndexOf(new Tuple<int, int>(j, i)) == 0)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.Write("  ");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.Write("  ");
                                Console.ResetColor();
                            }
                            currentIndex++;
                        }
                        // Print Edges
                        //else if (i == 0 || i == windowHeight - 1 || j == 0 || j == windowWidth - 1)
                        //{
                        //    Console.BackgroundColor = ConsoleColor.Gray;
                        //    Console.Write(" ");
                        //    Console.ResetColor();
                        //}
                        else
                        {
                            Console.Write("  ");
                        }
                        if (listCount > currentIndex && tempList[currentIndex].Item2 != i)
                        {
                            break;
                        }
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                }

            }
        }

        private static void PrintGameAreaPsychadelic()
        {
            List<Tuple<int, int>> tempList = coordinateList.OrderBy(a => a.Item2).ThenBy(b => b.Item1).ToList();

            for (int i = 0; i < tempList.Count; i++)
            {
                int numberOfLines = i == 0 ? tempList[i].Item2 : tempList[i].Item2 - tempList[i - 1].Item2;
                int numberOfSpaces = i == 0 || numberOfLines != 0 ? tempList[i].Item1 : tempList[i].Item1 - tempList[i - 1].Item1;

                ch.BlankLine(numberOfLines);
                StringBuilder sb = new StringBuilder();
                sb.Append(' ', numberOfSpaces);
                Console.Write(sb);

                if (coordinateList.IndexOf(new Tuple<int, int>(tempList[i].Item1, tempList[i].Item2)) == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("  ");
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("  ");
                    Console.ResetColor();
                }
            }
        }

        private static void PrintGameAreaTest()
        {
            List<Tuple<int, int>> tempList = coordinateList.OrderBy(a => a.Item2).ThenBy(b => b.Item1).ToList();

            for (int i = 0; i < tempList.Count; i++)
            {
                int numberOfLines = i == 0 ? tempList[i].Item2 : tempList[i].Item2 - tempList[i - 1].Item2;
                int numberOfSpaces = i == 0 || numberOfLines != 0 ? tempList[i].Item1 : tempList[i].Item1 - tempList[i - 1].Item1;

                ch.BlankLine(numberOfLines);
                StringBuilder sb = new StringBuilder();
                sb.Append(' ', numberOfSpaces);
                Console.Write(sb);

                if (coordinateList.IndexOf(new Tuple<int, int>(tempList[i].Item1, tempList[i].Item2)) == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("  ");
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("  ");
                    Console.ResetColor();
                }
            }
        }

    }
}
