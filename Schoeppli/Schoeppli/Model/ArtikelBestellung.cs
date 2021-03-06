﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    // Model für Bestellungen mit einem Artikel und der Anzahl, die bestellt wurde
    public class ArtikelBestellung
    {
        public int Artikelnummer { get; set; }
        public int Anzahl { get; set; }

        public ArtikelBestellung(int aNummer, int anzahl)
        {
            Artikelnummer = aNummer;
            Anzahl = anzahl;
        }
    }
}
