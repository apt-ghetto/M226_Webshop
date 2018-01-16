using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    // Interface für Klassen, welche eine View darstellen
    public interface IView
    {
        // Einstiegspunkt für eine View-Klasse
        void ShowView();

        // User-Menü anzeigen mit Auswahlpunkten
        void ShowMenu();
    }
}
