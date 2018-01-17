using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    /// <summary>
    /// Interface für Klassen, welche ein Model darstellen
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Alle wichtigen Attribute des Models retournieren
        /// </summary>
        /// <returns>String mit den Infos</returns>
        string GetInfoAll();
    }
}
