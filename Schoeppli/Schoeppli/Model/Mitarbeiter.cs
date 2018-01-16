using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    // Model für Mitarbeiter
    // Childklasse zu Person
    public class Mitarbeiter : Person
    {
        public Abteilung Abteilung { get; set; }
        public int Lohn { get; set; }
        public Status Status { get; set; }

        public Mitarbeiter(int id, string vorname, string nachname, DateTime geburtsdatum, string adresse, string plz, Abteilung abteilung, int lohn, Status status)
            :base(id, vorname, nachname, geburtsdatum, adresse, plz)
        {
            Abteilung = abteilung;
            Lohn = lohn;
            Status = status;
        }

        // Alle Infos eines Mitarbeiters auslesen
        public override string GetInfoAll()
        {
            string info = base.GetInfoAll();
            return info + $", Abteilung: {Abteilung}, Lohn: {Lohn}.-, Status: {Status}";
        }

        // Override ToString für schönere Darstellung
        public override string ToString()
        {
            string formattedString = $"ID: {ID}".PadRight(10) +
                $"Vorname: {Vorname}".PadRight(25) +
                $"Nachname: {Nachname}".PadRight(25) +
                $"Abteilung: {Abteilung}".PadRight(30) +
                $"Status: {Status}";

            return formattedString;
        }
    }
}
