using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CardGame.ClassLibrary
{
    public class PlayingCardDeck
    {
        ConsoleHelper ch = new ConsoleHelper();

        public List<PlayingCard> PlayingCards { get; set; }
        public int Length { get; internal set; }

        public PlayingCardDeck()
        {
            PlayingCards = new List<PlayingCard>();
            Length = 0;

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    PlayingCard playingCard = new PlayingCard(suit, value);
                    PlayingCards.Add(playingCard);
                    Length++;
                }
            }
        }

        public PlayingCard GetTopCard()
        {
            try
            {
                PlayingCard card = PlayingCards[PlayingCards.Count - 1];
                PlayingCards.RemoveAt(PlayingCards.Count - 1);
                Length--;
                return card;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(e.Message);
                ch.PressKeyToContinue();
                return null;
            }
            
        }

        public bool InsertBottomCard(PlayingCard card)
        {
            try
            {
                PlayingCards.Insert(0, card);
                Length++;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ch.PressKeyToContinue();
                throw;
            }
        }

        // Lets you use a foreach-loop over the deck
        public IEnumerator<PlayingCard> GetEnumerator()
        {
            foreach(PlayingCard card in PlayingCards)
            {
                yield return card;
            }
        }
    }

}
