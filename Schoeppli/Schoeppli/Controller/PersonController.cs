using Newtonsoft.Json;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Controller
{
    public class PersonController
    {
        const string filePath = @"C:\_Database\Personen.json";

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

        public void WriteToFile()
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, alleKunden);
            }
        }

        public void ReadFromFile()
        {
            alleKunden = new List<Kunde>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string data = reader.ReadToEnd();
                alleKunden = JsonConvert.DeserializeObject<List<Kunde>>(data);
            }


        }
    }
}
