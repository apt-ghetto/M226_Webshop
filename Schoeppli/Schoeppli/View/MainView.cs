using Schoeppli.Controller;
using Schoeppli.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    public class MainView : IView
    {
        private PersonController personenCtrl;
        private ProduktController produktCtrl;

        public MainView()
        {
            personenCtrl = new PersonController();
            produktCtrl = new ProduktController();
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
                            ProduktView produktView = new ProduktView(produktCtrl);
                            produktView.ShowView();
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
            Console.WriteLine("1) Mitarbeiterverwaltung");
            Console.WriteLine("2) Kundenverwaltung");
            Console.WriteLine("3) Produktverwaltung");
            Console.WriteLine("4) Bestellungen");
            Console.WriteLine("5) Rechnungswesen");
            Console.WriteLine();
            Console.WriteLine("9) Speichern und Beenden");
            Console.WriteLine();
        }
    }
}
