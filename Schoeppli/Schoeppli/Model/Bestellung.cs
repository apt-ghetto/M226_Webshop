﻿using Schoeppli.Interface;
using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    // Model für Bestellungen
    public class Bestellung : IModel
    {
        public int Bestellnummer { get; set; }
        public Kunde Besteller { get; set; }
        public DateTime Bestelldatum { get; set; }
        public Bestellstatus Bestellstatus { get; set; }
        public List<ArtikelBestellung> BestellteArtikel { get; set; }

        // Konstruktor
        public Bestellung(int bestellnummer, Kunde besteller, DateTime bestelldatum, Bestellstatus bestellstatus)
        {
            Bestellnummer = bestellnummer;
            Besteller = besteller;
            Bestelldatum = bestelldatum;
            Bestellstatus = bestellstatus;
            BestellteArtikel = new List<ArtikelBestellung>();
        }

        // Alle Infos zu einer Bestellung auslesen
        public string GetInfoAll()
        {
            return $"Besteller: {Besteller.Vorname} {Besteller.Nachname}, Bestelldatum: {Bestelldatum.ToShortDateString()}, Bestellstatus: {Bestellstatus}";
        }

        // Override ToString für schönere Darstellung
        public override string ToString()
        {
            return $"BstNr: {Bestellnummer}\tKunde: {Besteller.Vorname} {Besteller.Nachname}\tDatum: {Bestelldatum.ToShortDateString()}\tStatus: {Bestellstatus}\n";
        }
        
    }
}
