using Schoeppli.Generic;
using Schoeppli.Interface;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Controller
{
    public class BestellungController : IDataAccess
    {
        List<Bestellung> alleBestellungen;
        private static string bestellPath = @"C:\_Database\Bestellungen.json";
        private static string rechnungPath = @"C:\_Database\Rechnungen\";

        // Konstruktor
        public BestellungController()
        {
            ReadData();
        }
        
        // Daten aus File lesen
        public void ReadData()
        {
            alleBestellungen = DataAccess<Bestellung>.ReadFromFile(bestellPath);
            if (alleBestellungen == null)
            {
                alleBestellungen = new List<Bestellung>();
            }
        }

        // Daten in File schreiben
        public void WriteData()
        {
            DataAccess<Bestellung>.WriteToFile(alleBestellungen, bestellPath);
        }

        // Alle Bestellungen auslesen
        public List<Bestellung> GetAllBestellungen()
        {
            return alleBestellungen;
        }

        // Alle Rechnungen auslesen
        public string[] GetAllBills()
        {
            return Directory.GetFiles(rechnungPath);
        }

        // Neue Bestellung zur Liste hinzufügen
        public void SaveNewBestellung(Bestellung nigelnagelneueBestellung)
        {
            nigelnagelneueBestellung.Bestellnummer = alleBestellungen.Count == 0 ?
                1 :
                alleBestellungen[alleBestellungen.Count - 1].Bestellnummer + 1;

            alleBestellungen.Add(nigelnagelneueBestellung);
        }

        // Neue Rechnung als Datei erstellen
        public string CreateFileNewRechnung(Bestellung nigelnagelneueBestellung)
        {
            string returnString = string.Empty;
            DateTime now = DateTime.Now;

            string fileName = $"{rechnungPath}{now.ToString("yyyy-MM-dd_HH-mm")}_{nigelnagelneueBestellung.Bestellnummer}_{nigelnagelneueBestellung.Besteller.Nachname}.txt";

            if (!File.Exists(fileName))
            {
                returnString = fileName;
            }
            return returnString;
        }
    }
}
