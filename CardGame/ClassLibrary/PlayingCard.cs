using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public class PlayingCard
    {
        public Suit Suit { get; private set; }
        public Value Value { get; private set; }
        public bool IsVisible { get; set; }

        //public string[] CardAppearence { get; private set; }

        // Constructor
        public PlayingCard(Suit aSuit, Value aValue)
        {
            Suit = aSuit;
            Value = aValue;

            //string shortValue = GetValue(aValue);
            //string shortSuit = GetSuit(aSuit);

            //CardAppearence = new string[5]
            //{

            //    $@"_____",
            //    $@"|{shortSuit.PadRight(3)}|",
            //    $@"| {shortValue.PadRight(2)}|",
            //    $@"|{shortSuit.PadLeft(3)}|",
            //    $@"‾‾‾‾‾"
            //};
        }

        //private string GetSuit(Suit aSuit)
        //{
        //    string spades = "\u2660";
        //    string clubs = "\u2663";
        //    string hearts = "\u2665";
        //    string diamonds = "\u2666";

        //    string suit = "";
        //    switch (aSuit.ToString())
        //    {
        //        case "Spades":
        //            suit = spades;
        //            break;
        //        case "Clubs":
        //            suit = clubs;
        //            break;
        //        case "Hearts":
        //            suit = hearts;
        //            break;
        //        case "Diamonds":
        //            suit = diamonds;
        //            break;
        //        default:
        //            break;
        //    }
        //    return suit;
        //}

        //private string GetValue(Value aValue)
        //{
        //    string value = "";
        //    switch (aValue.ToString())
        //    {
        //        case "Ace":
        //            value = "A";
        //            break;
        //        case "King":
        //            value = "K";
        //            break;
        //        case "Queen":
        //            value = "Q";
        //            break;
        //        case "Jack":
        //            value = "J";
        //            break;
        //        default:
        //            value = ((int)aValue).ToString();
        //            break;
        //    }

        //    return value;
        //}
    }
}