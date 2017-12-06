using Schoeppli.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
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

        public override string ToString()
        {
            return $"ID: {ID}\tVorname: {Vorname}\tNachname: {Nachname}\n";
        }

        public virtual string GetInfoAll()
        {
            return $"ID: {ID}, Vorname: {Vorname}, Nachname: {Nachname}, Geburtsdatum: {Geburtsdatum}, Adresse: {Adresse}, PLZ: {PLZ}";
        }
    }
}
