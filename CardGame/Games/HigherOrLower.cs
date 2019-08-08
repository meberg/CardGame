using CardGame.ClassLibrary;
using CardGame.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Games
{
    public class HigherOrLower
    {
        public static readonly int GameId = 1;

        ConsoleHelper ch = new ConsoleHelper();
        PlayingCardPrinter pcp = new PlayingCardPrinter();
        DataAccess dataAccess = new DataAccess();
        int score = 0;

        string player = MainApp.currentUser.Username;

        public void Run()
        {
            ch.ResetColor();
            Menu();
        }

        private void Menu()
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                Console.Clear();

                ch.Menu($"Welcome to Higher or Lower {player}!");
                ch.BlankLine(2);

                Console.WriteLine("What do you want to do?");

                Console.WriteLine("a) Start game");
                Console.WriteLine("b) Change display name");

                Console.WriteLine("x) Back to previous menu");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        PlayGame();
                        break;
                    case ConsoleKey.B:
                        ChangeDisplayName();
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

        private void ChangeDisplayName()
        {
            Console.Clear();
            Console.WriteLine("Current display name: " + player);
            Console.WriteLine("Enter new display name: ");
            player = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Display name changed to " + player);
            ch.PressKeyToContinue();
        }

        PlayingCard playerCard;
        PlayingCard computerCard;
        PlayingCardDeck deck;

        private void PlayGame()
        {
            score = 0;

            deck = new PlayingCardDeck();
            deck.Shuffle();
            bool done = false;

            while (!done)
            {
                Console.Clear();

                bool winner = false;
                playerCard = deck.GetTopCard();
                computerCard = deck.GetTopCard();

                Console.WriteLine("Computer:");
                pcp.PrintCard(computerCard);

                Console.WriteLine();
                Console.WriteLine("Is your card higher or lower? (x to exit)");
                Console.WriteLine();
                Console.WriteLine("a) Higher");
                Console.WriteLine("b) Lower");
                Console.WriteLine("c) Tie");

                List<ConsoleKey> allowedKeys = new List<ConsoleKey>() { ConsoleKey.A, ConsoleKey.B, ConsoleKey.C, ConsoleKey.X };
                ConsoleKey answer;
                string userGuess = "";

                do
                {
                    answer = Console.ReadKey(true).Key;
                    if (!allowedKeys.Contains(answer))
                    {
                        ch.Red("That key is not allowed.");
                        ch.BlankLine();
                    }
                } while (!allowedKeys.Contains(answer));

                switch (answer)
                {
                    case ConsoleKey.A:
                        winner = (int)playerCard.Value > (int)computerCard.Value ? true : false;
                        userGuess = "Higher";
                        break;
                    case ConsoleKey.B:
                        winner = (int)playerCard.Value < (int)computerCard.Value ? true : false;
                        userGuess = "Lower";
                        break;
                    case ConsoleKey.C:
                        winner = (int)playerCard.Value == (int)computerCard.Value ? true : false;
                        userGuess = "Tie";
                        break;
                    case ConsoleKey.X:
                        done = true;
                        break;
                    default:
                        break;
                }

                if (!done)
                {
                    Console.Clear();
                    Console.WriteLine("Computer:");
                    pcp.PrintCard(computerCard);
                    Console.WriteLine("Player:");
                    pcp.PrintCard(playerCard);

                    if (winner)
                    {
                        Console.WriteLine("Correct!");
                        score++;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect guess");
                    }
                    Console.WriteLine("Your guess: " + userGuess);
                    Console.WriteLine("Score: " + score);
                    Console.WriteLine("Cards left in deck: " + deck.Length);

                    if (deck.Length == 0)
                    {
                        done = true;
                        ch.PressKeyToContinue();
                        Console.Clear();
                        Console.WriteLine("Game over!");
                        Console.WriteLine("You got " + score + " points out of 26 possible");

                        if (MainApp.isLoggedIn)
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
                    ch.PressKeyToContinue();
                }
            }
            Console.WriteLine();
        }
    }
}
