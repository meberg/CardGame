using CardGame.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Games
{
    public class TurningTen
    {
        // 2 spelare mot varandra på samma dator
        // 1 kortlek
        // Varje spelare får 3 dolda, 3 öppna, 3 på handen. Resten läggs i en hög.
        // 
        // Mellansteg då båda spelarnas kort är dolda. 
        // Spelare 1 börjar lägga.
        // Spelet är över då någon spelare har helt slut på kort. 


        string player1 = "player 1";
        string player2 = "player 2";

        List<PlayingCard> player1Hand;
        List<PlayingCard> player1Hidden;
        List<PlayingCard> player1Open;

        List<PlayingCard> player2Hand;
        List<PlayingCard> player2Hidden;
        List<PlayingCard> player2Open;

        List<PlayingCard> playingPile;

        PlayingCardDeck deck = new PlayingCardDeck();

        public void Run()
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                Console.Clear();

                Console.WriteLine("What do you want to do?");

                Console.WriteLine("a) Start game");
                Console.WriteLine("b) Other options...");

                Console.WriteLine("x) Back to previous menu");


                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        StartGame();
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
                        break;
                    default:
                        break;
                }
            }
        }

        private void StartGame()
        {
            deck.Shuffle();
            DealCards();

            bool gameOver = false;
            while (!gameOver)
            {
                // Print hidden hands - press key to continue
                // Player 1:s turn (Print own hand)
                // Check if valid move
                // Check if game over

                // Print hidden hands - press key to continue
                // Player 2:s turn (Print own hand)
                // Check if valid move
                // Check if game over

                
            }
        }

        private void DealCards()
        {
            for (int i = 0; i < 3; i++)
            {
                player1Hand.Add(deck.GetTopCard());
                player1Hidden.Add(deck.GetTopCard());
                player1Open.Add(deck.GetTopCard());
                player2Hand.Add(deck.GetTopCard());
                player2Hidden.Add(deck.GetTopCard());
                player2Open.Add(deck.GetTopCard());
            }
        }
    }
}
