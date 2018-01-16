using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    // Model für Kunden
    // Childklasse zu Person
    public class Kunde : Person
    {

        public string Kundennummer { get; set; }
        public Kunde(int id, string vorname, string nachname, DateTime geburtsdatum, string adresse, string plz, string kundennummer)
            :base(id, vorname, nachname, geburtsdatum, adresse, plz)
        {
            Kundennummer = kundennummer;
        }

        // Alle Infos zu eines Kunden auslesen
        public override string GetInfoAll()
        {
            string info =  base.GetInfoAll();
            return info + $", Kundennummer: {Kundennummer}";
        }
    }
}
