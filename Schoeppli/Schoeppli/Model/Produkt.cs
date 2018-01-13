﻿using Schoeppli.Interface;
using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    public class Produkt : IModel
    {        
        public int ID { get; set; }
        public string Beschreibung { get; set; }
        public Kategorie Kategorie { get; set; }
        public int Preis { get; set; }
        public int Bestand { get; set; }

        public Produkt()
        {
            // do not delete
        }

        public Produkt(int id, string beschreibung, Kategorie kategorie, int preis, int bestand)
        {
            ID = id;
            Beschreibung = beschreibung;
            Kategorie = kategorie;
            Preis = preis;
            Bestand = bestand;
        }

        public override string ToString()
        {
            string formattedString = $"ID: {ID}".PadRight(10) +
                $"Beschreibung: {Beschreibung}".PadRight(40) +
                $"Bestand: {Bestand}";

            return formattedString;
        }

        public string GetInfoAll()
        {
            string formattedString = 
                $"Beschreibung: {Beschreibung}".PadRight(40) +
                $"Kategorie: {Kategorie}".PadRight(30) +
                $"Preis: {Preis / 100}.{(Preis % 100).ToString("00")}".PadRight(20) +
                $"Bestand: {Bestand}";

            return formattedString;
        }
    }
}
