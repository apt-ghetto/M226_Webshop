using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    /// <summary>
    /// Interface für Klassen, welche eine View darstellen
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Einstiegspunkt für eine View-Klasse
        /// </summary>
        void ShowView();

        /// <summary>
        /// User-Menü anzeigen mit Auswahlpunkten
        /// </summary>
        void ShowMenu();
    }
}
