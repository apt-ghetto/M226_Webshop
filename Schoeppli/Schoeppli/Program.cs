using Schoeppli.Controller;
using Schoeppli.Generic;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonController pControl = new PersonController();

            pControl.InitializePeople();
            pControl.WriteData();
            pControl.ReadData();
            pControl.PrintPeople();
        }
    }
}
