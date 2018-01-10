using Schoeppli.Controller;
using Schoeppli.Interface;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    public class RechnungsView : IView
    {
        private BestellungController controller;

        public RechnungsView(BestellungController controller)
        {
            this.controller = controller;
        }

        public void ShowView()
        {
            byte input;
            do
            {
                ConsoleUtils.PrintTitle();
                ShowMenu();
                ConsoleUtils.PrintPrompt();
                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            ShowAll();
                            ConsoleUtils.PrintContinueMessage();
                            Console.ReadKey();
                            break;
                        case 2:
                            break;
                        case 9:

                            break;
                        default:
                            ConsoleUtils.PrintInvalidMessage();
                            break;
                    }
                }
                else
                {
                    ConsoleUtils.PrintInvalidMessage();
                }
            } while (input != 9);
        }

        public void ShowMenu()
        {
            Console.WriteLine("1) Rechnung anzeigen");
            Console.WriteLine("2) Rechnung erstellen");
            Console.WriteLine();
            Console.WriteLine("9) Zurück");
            Console.WriteLine();
        }

        public void ShowAll()
        {
            
        }


    }
}
