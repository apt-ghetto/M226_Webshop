using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    public static class ConsoleUtils
    {
        public static void PrintTitle()
        {
            Console.Clear();
            Console.WriteLine("***  ***  ***  ***  SCHÖPPLI  ***  ***  ***  ***\n\n");
        }

        public static void PrintPrompt()
        {
            Console.Write("SchöppliShell::> ");
        }

        public static void PrintInvalidMessage()
        {
            Console.WriteLine("Ungültige Eingabe.");
        }

        public static void PrintContinueMessage()
        {
            Console.Write("Beliebige Taste drücken...");
        }

        public static void PrintSaveTemporary()
        {
            Console.WriteLine("Änderung temporär übernehmen? y/n");
            PrintPrompt();
        }

        public static int GetUserInputAsInt(string userText)
        {
            int number;

            do
            {
                Console.Write(userText);

                if (Int32.TryParse(Console.ReadLine(), out number))
                {
                    return number;
                }
                else
                {
                    PrintInvalidMessage();
                }
            } while (true);
        }
    }
}
