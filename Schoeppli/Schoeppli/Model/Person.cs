using Schoeppli.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    // Model für Personen (Parentklasse)
    public abstract class Person : IModel
    {
        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public string Adresse { get; set; }
        public string PLZ { get; set; }

        public Person(int id, string vorname, string nachname, DateTime geburtsdatum, string adresse, string plz)
        {
            ID = id;
            Vorname = vorname;
            Nachname = nachname;
            Geburtsdatum = geburtsdatum;
            Adresse = adresse;
            PLZ = plz;
        }

        // Override ToString für schönere Darstellung
        public override string ToString()
        {
            string formattedString = $"ID: {ID}".PadRight(10) +
                $"Vorname: {Vorname}".PadRight(25) +
                $"Nachname: {Nachname}";

            return formattedString;
        }

        // Alle Infos einer Person auslesen
        public virtual string GetInfoAll()
        {
            return $"Vorname: {Vorname}, Nachname: {Nachname}, Geburtsdatum: {Geburtsdatum.ToShortDateString()}, Adresse: {Adresse}, PLZ: {PLZ}";
        }
    }
}
