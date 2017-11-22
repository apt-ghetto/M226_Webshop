using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    public class Kunde : Person
    {

        public string Kundennummer { get; set; }
        public Kunde(int id, string vorname, string nachname, DateTime geburtsdatum, string adresse, string plz, string kundennummer)
            :base(id, vorname, nachname, geburtsdatum, adresse, plz)
        {
            Kundennummer = kundennummer;
        }

        public static string GetFilePath()
        {
            return @"C:\_Database\Kunden.json";
        }


    }
}
