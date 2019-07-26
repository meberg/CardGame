using System;

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
    }
}