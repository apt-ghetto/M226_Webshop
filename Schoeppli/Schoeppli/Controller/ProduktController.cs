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
        private static string filePath = @"C:\_Database\Produkte.json";
        List<Produkt> alleProdukte;

        // Konstruktor
        public ProduktController()
        {
            ReadData();
        }

        // Daten in File schreiben
        public void WriteData()
        {
            DataAccess<Produkt>.WriteToFile(alleProdukte, filePath);
        }

        // Daten aus File lesen
        public void ReadData()
        {
            alleProdukte = DataAccess<Produkt>.ReadFromFile(filePath);
        }

        // Alle Produkte auslesen
        public List<Produkt> GetAllProducts()
        {
            return alleProdukte;
        }

        // Neues Produkt in Liste speichern
        public void SaveNewProduct(Produkt nigelnagelneuesProdukt)
        {
            if (alleProdukte != null)
            {
                nigelnagelneuesProdukt.ID = alleProdukte[alleProdukte.Count - 1].ID + 1;
            }
            else
            {
                alleProdukte = new List<Produkt>();
                nigelnagelneuesProdukt.ID = 1;
            }

            alleProdukte.Add(nigelnagelneuesProdukt);
        }
    }
}