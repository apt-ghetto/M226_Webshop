using Schoeppli.Controller;
using Schoeppli.Generic;
using Schoeppli.Model;
using Schoeppli.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli
{
    class Program
    {
        private static string dbPath = @"C:\_Database";

        static void Main(string[] args)
        {
            // check if db path exists, create if not
            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);                
            }

            MainView view = new MainView();
            view.ShowView();
        }
    }
}
