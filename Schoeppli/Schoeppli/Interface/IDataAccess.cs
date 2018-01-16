using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    // Interface für alle Klassen, welche über einen Datenzugriff verfügen müssen
    interface IDataAccess
    {
        // Daten in ein File schreiben
        void WriteData();

        // Daten aus einem File lesen
        void ReadData();
    }
}
