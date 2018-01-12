﻿using Schoeppli.Generic;
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

        public BestellungController()
        {
            ReadData();
        }
        
        public void ReadData()
        {
            alleBestellungen = DataAccess<Bestellung>.ReadFromFile(bestellPath);
            if (alleBestellungen == null)
            {
                alleBestellungen = new List<Bestellung>();
            }
        }

        public void WriteData()
        {
            DataAccess<Bestellung>.WriteToFile(alleBestellungen, bestellPath);
        }

        public List<Bestellung> GetAllBestellungen()
        {
            return alleBestellungen;
        }

        public string[] GetAllBills()
        {
            return Directory.GetFiles(rechnungPath);
        }

        public void SaveNewBestellung(Bestellung nigelnagelneueBestellung)
        {
            nigelnagelneueBestellung.Bestellnummer = alleBestellungen.Count == 0 ?
                1 :
                alleBestellungen[alleBestellungen.Count - 1].Bestellnummer + 1;

            alleBestellungen.Add(nigelnagelneueBestellung);
        }

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
