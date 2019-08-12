using System;
using System.Text;

namespace CardGame
{
    internal class ConsoleHelper
    {

        public ConsoleHelper()
        {
        }

        // If no arguments are provided, it uses 1 as argument
        internal void BlankLine(int numberOfLines = 1)
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.WriteLine();
            }
        }

        public void PressKeyToContinue()
        {
            Console.Write("Press a key to continue...");
            Console.ReadKey(true);
        }

        public void Red(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            ResetColor();
        }

        public void Green(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            ResetColor();
        }

        public void ResetColor()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void Menu(string text = "This is a super fun game!")
        {
            int width = Console.WindowWidth - 1;
            StringBuilder sb = new StringBuilder();

            string header = sb.Append('*', width).ToString();
            string sidebar = "***";

            Console.WriteLine(header);

            Console.WriteLine(sidebar.PadRight(width-3) + sidebar);

            Console.Write(sidebar);
            Console.Write($"{text.PadLeft((width+text.Length-6)/2)}");
            Console.WriteLine("***".PadLeft((width-text.Length+1)/2));

            Console.WriteLine(sidebar.PadRight(width - 3) + sidebar);

            Console.WriteLine(header);


        }

        internal void WindowSize(int width, int height)
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;
        }
    }
}