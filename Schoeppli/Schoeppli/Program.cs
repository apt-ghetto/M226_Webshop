using Schoeppli.Controller;
using Schoeppli.Generic;
using Schoeppli.Model;
using Schoeppli.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli
{
    // Hauptklasse und Einstiegspunkt der Applikation
    class Program
    {
        // Pfade für Ablage der Datenbank
        private static string dbPath = @"C:\_Database";
        private static string billPath = $@"{dbPath}\Rechnungen";

        // Einstiegsmethode der Applikation
        static void Main(string[] args)
        {
            // Datebankpfad erstellen, wenn er noch nicht existiert
            if (!Directory.Exists(billPath))
            {
                Directory.CreateDirectory(billPath);
            }

            // Hauptmenü anzeigen
            MainView view = new MainView();
            view.ShowView();
        }
    }
}
