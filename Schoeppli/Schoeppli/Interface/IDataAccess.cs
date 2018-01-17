using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    /// <summary>
    /// Interface für alle Klassen, welche über einen Datenzugriff verfügen müssen
    /// </summary>
    interface IDataAccess
    {
        /// <summary>
        /// Daten in ein File zu schreiben
        /// </summary>
        void WriteData();

        /// <summary>
        /// Daten aus einem File lesen
        /// </summary>
        void ReadData();
    }
}
