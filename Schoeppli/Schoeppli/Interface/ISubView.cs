using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    /// <summary>
    /// Interface für Klassen, welche ein Model darstellen, die eine Liste enthalten.
    /// Somit können diese Klassen alle Objekte der Liste anzeigen (in Konsole)
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    public interface ISubView<T> : IView
    {
        /// <summary>
        /// Alle Elemente der Liste anzeigen
        /// </summary>
        /// <param name="items">Objektliste</param>
        void ShowAll(List<T> items);

        /// <summary>
        /// Ein einzelnes Element anzeigen
        /// </summary>
        void ShowSingle();
    }
}
