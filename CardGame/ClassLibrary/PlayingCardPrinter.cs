using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.ClassLibrary
{
    class PlayingCardPrinter
    {
        ConsoleHelper ch = new ConsoleHelper();

        public static string[] Padding { get; private set; } = new string[5] {
            @" ",
            @" ",
            @" ",
            @" ",
            @" "
        };

        public static string[] HiddenCard { get; private set; } = new string[5] {
            @"_____",
            @"|/ \|",
            @"| ¤ |",
            @"|\ /|",
            @"‾‾‾‾‾"
        };

        public static string[] HiddenDeck { get; private set; } = new string[5] {
            @"_______",
            @"|||/ \|",
            @"||| ¤ |",
            @"|||\ /|",
            @"‾‾‾‾‾‾‾"
        };

        public void PrintCards(List<PlayingCard> cards)
        {
            ch.ResetColor();

            //Print top
            foreach (var card in cards)
            {
                if (card.IsEmptyCard)
                {
                    Console.Write($"     ".PadRight(6));
                }
                else
                {
                    Console.Write($"_____".PadRight(6));
                }
            }
            ch.BlankLine();

            Console.OutputEncoding = Encoding.Unicode;

            foreach (var card in cards)
            {
                if (card.IsEmptyCard)
                {
                    Console.Write($"     ".PadRight(6));
                }
                else
                {
                    string spades = "\u2660";
                    string clubs = "\u2663";
                    string hearts = "\u2665";
                    string diamonds = "\u2666";

                    string suit = " ";
                    switch (card.Suit.ToString())
                    {
                        case "Spades":
                            suit = spades;
                            break;
                        case "Clubs":
                            suit = clubs;
                            break;
                        case "Hearts":
                            suit = hearts;
                            break;
                        case "Diamonds":
                            suit = diamonds;
                            break;
                        default:
                            break;
                    }

                    Console.Write($"|");

                    if (card.Suit.ToString() == "Spades" || card.Suit.ToString() == "Clubs")
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"{suit}  ");
                    ch.ResetColor();
                    Console.Write($"|".PadRight(2));
                }
            }
            ch.BlankLine();

            foreach (var card in cards)
            {
                if (card.IsEmptyCard)
                {
                    Console.Write($"     ".PadRight(6));
                }
                else
                {
                    string value = " ";
                    switch (card.Value.ToString())
                    {
                        case "Ace":
                            value = "A";
                            break;
                        case "King":
                            value = "K";
                            break;
                        case "Queen":
                            value = "Q";
                            break;
                        case "Jack":
                            value = "J";
                            break;
                        default:
                            value = ((int)card.Value).ToString();
                            break;
                    }
                    Console.Write($"| {value.PadRight(2)}|".PadRight(6));
                }
            }
            ch.BlankLine();

            foreach (var card in cards)
            {
                if (card.IsEmptyCard)
                {
                    Console.Write($"     ".PadRight(6));
                }
                else
                {
                    string spades = "\u2660";
                    string clubs = "\u2663";
                    string hearts = "\u2665";
                    string diamonds = "\u2666";

                    string suit = "";
                    switch (card.Suit.ToString())
                    {
                        case "Spades":
                            suit = spades;
                            break;
                        case "Clubs":
                            suit = clubs;
                            break;
                        case "Hearts":
                            suit = hearts;
                            break;
                        case "Diamonds":
                            suit = diamonds;
                            break;
                        default:
                            break;
                    }

                    Console.Write($"|");

                    if (card.Suit.ToString() == "Spades" || card.Suit.ToString() == "Clubs")
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"  {suit}");
                    ch.ResetColor();
                    Console.Write($"|".PadRight(2));
                }
                
            }
            ch.BlankLine();

            foreach (var card in cards)
            {
                if (card.IsEmptyCard)
                {
                    Console.Write($"     ".PadRight(6));
                }
                else
                {
                    Console.Write($"‾‾‾‾‾".PadRight(6));

                }
            }
            ch.BlankLine();
        }

        public void PrintCard(PlayingCard card)
        {
            ch.ResetColor();

            //Print top
             Console.Write($"_____".PadRight(6));
            ch.BlankLine();

            Console.OutputEncoding = Encoding.Unicode;

            string spades = "\u2660";
            string clubs = "\u2663";
            string hearts = "\u2665";
            string diamonds = "\u2666";

            string suit = " ";
            switch (card.Suit.ToString())
            {
                case "Spades":
                    suit = spades;
                    break;
                case "Clubs":
                    suit = clubs;
                    break;
                case "Hearts":
                    suit = hearts;
                    break;
                case "Diamonds":
                    suit = diamonds;
                    break;
                default:
                    break;
            }

            Console.Write($"|");

            if (card.Suit.ToString() == "Spades" || card.Suit.ToString() == "Clubs")
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write($"{suit}  ");
            ch.ResetColor();
            Console.Write($"|".PadRight(2));
            
            ch.BlankLine();

            string value = " ";
            switch (card.Value.ToString())
            {
                case "Ace":
                    value = "A";
                    break;
                case "King":
                    value = "K";
                    break;
                case "Queen":
                    value = "Q";
                    break;
                case "Jack":
                    value = "J";
                    break;
                default:
                    value = ((int)card.Value).ToString();
                    break;
            }

            Console.Write($"| {value.PadRight(2)}|".PadRight(6));
            ch.BlankLine();

            Console.Write($"|");

            if (card.Suit.ToString() == "Spades" || card.Suit.ToString() == "Clubs")
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write($"  {suit}");
            ch.ResetColor();
            Console.WriteLine($"|".PadRight(2));

            Console.WriteLine($"‾‾‾‾‾".PadRight(6));
        }

        public void PrintListOfStringArrays(List<string[]> stringArrayList)
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (var item in stringArrayList)
                {
                    Console.WriteLine(item[i]);
                }
            }
        }



    }


}
