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
                ImportTestData();
            }

            // Hauptmenü anzeigen
            MainView view = new MainView();
            view.ShowView();
        }

        // Weil dies ein Schulprojekt ist und sicher niemand Lust hat zuerst einige
        // Testdaten zu kreieren, werden hier bereits vorgefertigte Dateien aus
        // dem Projekt selbst importiert.
        static void ImportTestData()
        {
            foreach (string file in Directory.GetFiles(@"Demo\Testdaten"))
            {                
                File.Copy(file, $@"{dbPath}/{Path.GetFileName(file)}");
            }

            foreach (string file in Directory.GetFiles(@"Demo\Testdaten\Rechnungen"))
            {
                File.Copy(file, $@"{billPath}/{Path.GetFileName(file)}");
            }

        }
    }
}
