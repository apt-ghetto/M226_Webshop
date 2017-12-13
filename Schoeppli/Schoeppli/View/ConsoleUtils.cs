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


    }
}
