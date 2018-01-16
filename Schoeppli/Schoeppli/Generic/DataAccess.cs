using Newtonsoft.Json;
using Schoeppli.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Generic
{
    // Generische Klasse für Datenzugriff auf Files
    public class DataAccess<T>
    {
        // Liste von Objekten in ein File schreiben und ablegen
        public static void WriteToFile(List<T> allObjects, string filePath)
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, allObjects);
            }
        }

        // Daten aus einem File lesen und in eine Liste von Objekten abfüllen
        public static List<T> ReadFromFile(string filePath)
        {
            List<T> allObjects = new List<T>();

            // Exception Handling
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string data = reader.ReadToEnd();
                    allObjects = JsonConvert.DeserializeObject<List<T>>(data);
                }
            }
            catch (FileNotFoundException)
            {
                FileStream file = File.Create(filePath);
                file.Close();
            }

            return allObjects;
        }

    }
}
