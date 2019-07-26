using CardGame.ClassLibrary;
using System;

namespace CardGame
{
    internal class MainApp
    {
        ConsoleHelper ch = new ConsoleHelper();

        public MainApp()
        {
        }

        internal void Run()
        {
            TestDisplayCards();
        }

        private void TestDisplayCards()
        {
            PlayingCardDeck cardDeck = new PlayingCardDeck();
            PrintCardDeck(cardDeck);
            ch.BlankLine();

            PlayingCard card = cardDeck.GetTopCard();
            PrintCard(card);
            ch.BlankLine();

            PrintCardDeck(cardDeck);
            ch.BlankLine();

            cardDeck.InsertBottomCard(card);
            PrintCardDeck(cardDeck);
            ch.BlankLine();
        }

        private void PrintCard(PlayingCard card)
        {
            Console.WriteLine($"{card.Value.ToString()} of " +
                    $"{card.Suit.ToString()}");
        }

        private void PrintCardDeck(PlayingCardDeck cardDeck)
        {
            foreach (var card in cardDeck)
            {
                Console.WriteLine($"{card.Value.ToString()} of " +
                    $"{card.Suit.ToString()}");
            }
            Console.WriteLine($"Number of cards: {cardDeck.Length}");
        }
    }
}