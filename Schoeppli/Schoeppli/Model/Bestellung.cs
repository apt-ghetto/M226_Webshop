using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    public class Bestellung
    {
        public int Bestellnummer { get; set; }
        public Kunde Besteller { get; set; }
        public DateTime Bestelldatum { get; set; }
        public Bestellstatus Bestellstatus { get; set; }
        public List<ArtikelBestellung> BestellteArtikel { get; set; }

        public Bestellung(int bestellnummer, Kunde besteller, DateTime bestellDatum, Bestellstatus bestellstatus)
        {
            Bestellnummer = bestellnummer;
            Besteller = besteller;
            Bestelldatum = Bestelldatum;
            Bestellstatus = bestellstatus;
            BestellteArtikel = new List<ArtikelBestellung>();
        }
    }
}
