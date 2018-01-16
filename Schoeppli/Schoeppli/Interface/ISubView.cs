using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    // Interface für Klassen, welche ein Model darstellen, die eine Liste enthalten
    // somit können diese Klassen alle Objekte der Liste anzeigen (in Konsole)
    public interface ISubView<T> : IView
    {
        void ShowAll(List<T> items);

        void ShowSingle();
    }
}
