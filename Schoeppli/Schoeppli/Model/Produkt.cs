using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    public class Produkt
    {        
        public int ID { get; set; }
        public string Beschreibung { get; set; }
        public Kategorie Kategorie { get; set; }
        public int Preis { get; set; }
        public Lagerplatz Lagerplatz { get; set; }
        public int Bestand { get; set; }
        public int MinAnzahl { get; set; }
        public int MaxAnzahl { get; set; }

        public Produkt(int id, string beschreibung, Kategorie kategorie, int preis, Lagerplatz lagerplatz, int bestand, int minAnzahl, int maxAnzahl)
        {
            ID = id;
            Beschreibung = beschreibung;
            Kategorie = kategorie;
            Preis = preis;
            Lagerplatz = lagerplatz;
            Bestand = bestand;
            MinAnzahl = minAnzahl;
            MaxAnzahl = maxAnzahl;
        }

        public static string GetFilePath()
        {
            return @"C:\_Database\Produkte.json";
        }

        public override string ToString()
        {
            return $"ID: {ID}\tBeschreibung: {Beschreibung}\tBestand: {Bestand}\n";
        }
    }
}
