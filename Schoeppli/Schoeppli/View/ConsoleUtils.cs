using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    // Helferklasse für oft verwendete Consolenfunktionen
    public static class ConsoleUtils
    {
        // Titel der Anwendung ausgeben und Screen clearen
        public static void PrintTitle()
        {
            Console.Clear();
            Console.WriteLine("***  ***  ***  ***  SCHÖPPLI  ***  ***  ***  ***\n\n");
        }

        // Aufforderung für den Benutzer, etwas einzugeben
        public static void PrintPrompt()
        {
            Console.Write("SchöppliShell::> ");
        }

        // Warnmeldung bei ungültiger Eingabe
        public static void PrintInvalidMessage()
        {
            Console.WriteLine("Ungültige Eingabe.");
        }

        // Aufforderung an den Benutzer, eine beliebige Taste zu drücken
        public static void PrintContinueMessage()
        {
            Console.Write("Beliebige Taste drücken...");
            Console.ReadKey();
        }

        // Aufforderung zum temporären Speichern
        public static void PrintSaveTemporary()
        {
            Console.WriteLine("Änderung temporär übernehmen? y/n");
            PrintPrompt();
        }

        /// <summary>
        /// Methode um dem Benutzer einen Text anzuzeigen
        /// und seine Auswahl in einen integer umzuwandeln und zu retournieren
        /// </summary>
        /// <param name="userText">Dem Benutzer anzuzeigenden Text</param>
        /// <returns>Auswahl des Benutzers</returns>
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
