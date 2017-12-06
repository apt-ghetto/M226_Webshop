using Schoeppli.Generic;
using Schoeppli.Interface;
using Schoeppli.Model;
using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Controller
{
    class ProduktController : IDataAccess
    {
        List<Produkt> alleProdukte;

        // Only for creating test data
        public void InitialiseProducts()
        {
            alleProdukte = new List<Produkt>();
            Produkt produkt = new Produkt(1, "Gigigampfi",
                Kategorie.Garten, 800000,
                Lagerplatz.D, 12, 0, 5);
            alleProdukte.Add(produkt);
            produkt = new Produkt(2, "Absinth 1L", Kategorie.Alkohol,
                2400, Lagerplatz.A, 18, 5, 120);
            alleProdukte.Add(produkt);

        }

        public void PrintProdukte()
        {
            foreach(Produkt p in alleProdukte)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public void WriteData()
        {
            DataAccess<Produkt>.WriteToFile(alleProdukte, Produkt.GetFilePath());
        }

        public void ReadData()
        {
            alleProdukte = DataAccess<Produkt>.ReadFromFile(Produkt.GetFilePath());
        }
    }
}
