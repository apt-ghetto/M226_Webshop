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
    public class DataAccess<T>
    {

        public static void WriteToFile(List<T> allObjects, string filePath)
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, allObjects);
            }
        }

        public static List<T> ReadFromFile(string filePath)
        {
            List<T> allObjects = new List<T>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string data = reader.ReadToEnd();
                allObjects = JsonConvert.DeserializeObject<List<T>>(data);
            }

            return allObjects;
        }

    }
}
