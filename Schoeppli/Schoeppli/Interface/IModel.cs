using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    // Interface für Klassen, welche ein Model darstellen
    public interface IModel
    {
        // Alle Eigenschaften des Models anzeigen (in Konsole schreiben)
        string GetInfoAll();
    }
}
