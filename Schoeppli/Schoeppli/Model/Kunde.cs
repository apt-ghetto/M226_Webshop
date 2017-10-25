using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Model
{
    public class Kunde : Person
    {        

        public Kunde(int id, string vorname, string nachname, DateTime geburtsdatum, string adresse, string plz)
            :base(id, vorname, nachname, geburtsdatum, adresse, plz)
        {
        }

    }
}
