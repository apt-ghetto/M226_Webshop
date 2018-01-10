using Newtonsoft.Json;
using Schoeppli.Generic;
using Schoeppli.Interface;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Controller
{
    public class PersonController : IDataAccess
    {
        private static string mitarbeiterFilePath = @"C:\_Database\Mitarbeiter.json";
        private static string kundenFilePath = @"C:\_Database\Kunden.json";

        List<Kunde> alleKunden;
        List<Mitarbeiter> alleMitarbeiter;

        public PersonController()
        {
            ReadData();
        }

        public void WriteData()
        {
            DataAccess<Kunde>.WriteToFile(alleKunden, kundenFilePath);
            DataAccess<Mitarbeiter>.WriteToFile(alleMitarbeiter, mitarbeiterFilePath);
        }

        public void ReadData()
        {
            alleKunden = DataAccess<Kunde>.ReadFromFile(kundenFilePath);
            alleMitarbeiter = DataAccess<Mitarbeiter>.ReadFromFile(mitarbeiterFilePath);
            if (alleMitarbeiter == null)
            {
                alleMitarbeiter = new List<Mitarbeiter>();
            }
            if (alleKunden == null)
            {
                alleKunden = new List<Kunde>();
            }
        }

        public List<Mitarbeiter> GetAllMitarbeiter()
        {
            return alleMitarbeiter;
        }

        public List<Kunde> GetAllKunden()
        {
            return alleKunden;

        }

        public void SaveNewMitarbeiter(Mitarbeiter nigelnagelneuerMitarbeiter)
        {
            nigelnagelneuerMitarbeiter.ID = alleMitarbeiter.Count == 0 ?
                1 :
                alleMitarbeiter[alleMitarbeiter.Count - 1].ID + 1;

            alleMitarbeiter.Add(nigelnagelneuerMitarbeiter);
        }

        public void SaveNewKunde(Kunde nigelnagelneuerKunde)
        {
            nigelnagelneuerKunde.ID = alleKunden.Count == 0 ?
                1 :
                alleKunden[alleKunden.Count - 1].ID + 1;

            alleKunden.Add(nigelnagelneuerKunde);
        }

    }
}
