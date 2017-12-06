using Schoeppli.Controller;
using Schoeppli.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    public class ProduktView : IView
    {
        private ProduktController controller;

        public ProduktView(ProduktController controller)
        {
            this.controller = controller;
        }

        public void ShowView()
        {
            byte input;
            do
            {
                Console.Clear();
                ConsoleUtils.PrintTitle();
                ShowMenu();
                ConsoleUtils.PrintUserInput();
                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:

                            break;
                        case 2:

                            break;
                        case 3:
                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 9:
                            
                            break;
                        default:
                            Console.WriteLine("Heitere Fahne.");
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
            Console.WriteLine("1) Neues Produkt erstellen");
            Console.WriteLine("2) ");
            Console.WriteLine("3) ");
            Console.WriteLine("4) ");
            Console.WriteLine("5) ");
            Console.WriteLine();
            Console.WriteLine("9) Zurück");
            Console.WriteLine();
        }
    }
}
