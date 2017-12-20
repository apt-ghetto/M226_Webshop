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
    public class ProduktController : IDataAccess
    {
        List<Produkt> alleProdukte;

        public ProduktController()
        {
            ReadData();
        }

        // Only for creating test data
        public void InitialiseProducts()
        {
            alleProdukte = new List<Produkt>();
            Produkt produkt = new Produkt(1, "Gigigampfi",
                Kategorie.Garten, 800000,
                12, 0, 5);
            alleProdukte.Add(produkt);
            produkt = new Produkt(2, "Absinth 1L", Kategorie.Alkohol,
                2400, 18, 5, 120);
            alleProdukte.Add(produkt);

        }

        public void WriteData()
        {
            DataAccess<Produkt>.WriteToFile(alleProdukte, Produkt.GetFilePath());
        }

        public void ReadData()
        {
            alleProdukte = DataAccess<Produkt>.ReadFromFile(Produkt.GetFilePath());
        }

        public List<Produkt> GetAllProducts()
        {
            return alleProdukte;
        }

        public void SaveNewProduct(Produkt nigelnagelneuesProdukt)
        {
            nigelnagelneuesProdukt.ID = alleProdukte[alleProdukte.Count - 1].ID + 1;

            alleProdukte.Add(nigelnagelneuesProdukt);
        }
    }
}