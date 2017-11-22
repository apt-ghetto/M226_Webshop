using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    interface IDataAccess
    {
        void WriteData();
        void ReadData();
    }
}
