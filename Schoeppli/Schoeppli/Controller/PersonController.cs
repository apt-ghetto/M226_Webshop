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
        //DataAccess<Person> personAccess = new DataAccess<Person>();
        List<Kunde> alleKunden;
        List<Mitarbeiter> alleMitarbeiter;

        public void AddPerson(Kunde person)
        {
            alleKunden.Add(person);
        }

        //For Debugging
        public void InitializePeople()
        {
            alleKunden = new List<Kunde>();
            Kunde testPerson = new Kunde(1, "Heinz", "Ketchup", DateTime.Now.AddMonths(-2560), "Strasse1", "23094", "09283450821");
            alleKunden.Add(testPerson);
            testPerson = new Kunde(2, "Madeleine", "Franzi", DateTime.Now.AddMonths(-1202), "Strasse2", "45862", "28672854234");
            alleKunden.Add(testPerson);
            testPerson = new Kunde(3, "Petra", "Strub", DateTime.Now.AddMonths(-500), "Strasse3", "4658", "54161875416");
            alleKunden.Add(testPerson);
        }

        //For Debugging
        public void PrintPeople()
        {
            foreach (Kunde p in alleKunden)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public void WriteData()
        {
            DataAccess<Kunde>.WriteToFile(alleKunden, Kunde.GetFilePath());
            DataAccess<Mitarbeiter>.WriteToFile(alleMitarbeiter, Mitarbeiter.GetFilePath());
        }

        public void ReadData()
        {
            alleKunden = DataAccess<Kunde>.ReadFromFile(Kunde.GetFilePath());
            alleMitarbeiter = DataAccess<Mitarbeiter>.ReadFromFile(Mitarbeiter.GetFilePath());
        }

    }
}
