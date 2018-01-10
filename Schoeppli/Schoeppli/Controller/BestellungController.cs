using Schoeppli.Generic;
using Schoeppli.Interface;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Controller
{
    public class BestellungController : IDataAccess
    {
        List<Bestellung> alleBestellungen;
        private static string bestellPath = @"C:\_Database\Bestellungen.json";

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

        public void SaveNewBestellung(Bestellung nigelnagelneueBestellung)
        {
            nigelnagelneueBestellung.Bestellnummer = alleBestellungen.Count == 0 ?
                1 :
                alleBestellungen[alleBestellungen.Count - 1].Bestellnummer + 1;

            alleBestellungen.Add(nigelnagelneueBestellung);
        }
    }
}
