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

        public ProduktController()
        {
            ReadData();
        }

        public void WriteData()
        {
            DataAccess<Produkt>.WriteToFile(alleProdukte, filePath);
        }

        public void ReadData()
        {
            alleProdukte = DataAccess<Produkt>.ReadFromFile(filePath);
        }

        public List<Produkt> GetAllProducts()
        {
            return alleProdukte;
        }

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